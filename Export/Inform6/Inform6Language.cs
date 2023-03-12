using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Trizbort.Domain;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;
using Trizbort.Domain.Misc;
using Trizbort.Export.Domain;
using Trizbort.Extensions;

namespace Trizbort.Export.Inform6;

public sealed class Inform6Language : Language {
  #region Readonly Fields

  private readonly Inform6Dialect dialect;

  #endregion

  #region Constructors

  public Inform6Language(Inform6Dialect dialect = Inform6Dialect.I6) {
    this.dialect = dialect;
  }

  #endregion

  #region Public Methods

  public override string ExportName(string displayName, int? suffix) {
    var name = DeAccent(StripUnaccentedCharacters(displayName)).Replace(" ", "").Replace("-", "");
    if (string.IsNullOrEmpty(name)) name = "item";
    if (suffix != null) name = $"{name}{suffix}";
    return name;
  }

  public override string ExportName(Room room, int? suffix) {
    var name = DeAccent(StripUnaccentedCharacters(room.Name)).Replace(" ", "").Replace("-", "");
    if (string.IsNullOrEmpty(name)) name = "room";
    if (suffix != null) name = $"{name}{suffix}";
    return name;
  }

  public override void WriteAbout(TextWriter writer) {
    writer.WriteLine("Verb meta 'about' * -> About;");
    writer.WriteLine();
    writer.WriteLine("[ AboutSub ;");
    writer.WriteLine("  print({0});", toI6String(Project.Current.History, DOUBLE_QUOTE));
    writer.WriteLine("];");
    writer.WriteLine();
  }

  public override void WriteHeader(TextWriter writer, string title, string author, string description) {
    writer.WriteLine("Constant Story {0};", toI6String(title, DOUBLE_QUOTE));
    writer.WriteLine("Constant Headline {0};", toI6String($"^By {author}^{description}^^", DOUBLE_QUOTE));
    writer.WriteLine();
    writer.WriteLine("Include \"Parser\";");
    writer.WriteLine("Include \"VerbLib\";");
    writer.WriteLine();
  }

  public override void WriteIncludes(TextWriter writer) {
    writer.WriteLine("Include \"Grammar\";");
  }

  public override void WriteInitialize(TextWriter writer, IEnumerable<Location> locationsInExportOrder) {
    writer.WriteLine("[ Initialise;");
    var inExportOrder = locationsInExportOrder.ToList();
    if (inExportOrder.Any()) {
      var foundStart = false;
      foreach (var exportName in inExportOrder.Where(location => location.Room.IsStartRoom)
                                              .Select(p => p.ExportName))
        if (foundStart) {
          writer.WriteLine("! {0} is a second start-room according to Trizbort.", exportName);
        }
        else {
          writer.WriteLine("    location = {0};", exportName);
          foundStart = true;
        }

      if (!foundStart)
        writer.WriteLine("    location = {0};", inExportOrder.FirstOrDefault()?.ExportName);
    }
    else {
      writer.WriteLine("    ! location = ...;");
    }

    writer.WriteLine("    ! \"^^Your opening paragraph here...^^\";");
    writer.WriteLine("];");
    writer.WriteLine();
  }

  public override void WriteLocations(TextWriter writer, IEnumerable<Location> locationsInExportOrder) {
    foreach (var location in locationsInExportOrder) {
      // export the location object
      writeLocation(writer, location);

      // export the doors from this location.
      writeDoors(writer, location);

      // export the objects in this location
      exportThings(writer, location.Things, null, 1);
    }
  }

  public override void WriteRegions(TextWriter writer, IEnumerable<ExportRegion> regionsInExportOrder) {
    foreach (var region in regionsInExportOrder) writer.WriteLine("Class {0};", region.ExportName);
    writer.WriteLine();
  }

  #endregion

  #region Methods

  private static void exportThings(TextWriter writer, IEnumerable<Thing> things, Thing container, int indent) {
    foreach (var thing in things.Where(thing => thing.Container == container)) {
      writeOneThing(writer, thing, indent, container);
      exportThings(writer, thing.Contents, thing, indent + 1);
    }
  }

  private static List<string> setAttributes(Thing thing) {
    var attributes = new List<string>();

    if (thing.Contents.Count > 0) {
      if (thing.Contents.Any(item => item.PartOf)) {
        attributes.Add("transparent");
      }
      else {
        attributes.Add("open");
        attributes.Add("container");
      }
    }

    if (thing.ProperNamed) attributes.Add("proper");

    if (thing.IsPerson) {
      attributes.Add("animate");

      switch (thing.Gender) { // this may not be entirely true, but for now only animate objects will have a gender.
        case Thing.ThingGender.Female:
          attributes.Add("female");
          break;
        case Thing.ThingGender.Male:
          attributes.Add("male");
          break;
        case Thing.ThingGender.Neuter:
          attributes.Add("neuter");
          break;
      }
    }

    if (thing.IsScenery) attributes.Add("scenery");

    if (thing.IsSupporter) attributes.Add("supporter");

    if (thing.IsContainer && !attributes.Contains("container")) attributes.Add("container");

    if (thing.Forceplural == Thing.Amounts.Plural) attributes.Add("pluralname");

    // in I6 the worn attribute only applies to the player character, so we'll just mark this as clothing since we don't have a way to deal with the player.
    if (thing.Worn) attributes.Add("clothing");

    return attributes;
  }

  private static string toI6PropertyName(MappableDirection direction) {
    switch (direction) {
      case MappableDirection.North:
        return "n_to";
      case MappableDirection.South:
        return "s_to";
      case MappableDirection.East:
        return "e_to";
      case MappableDirection.West:
        return "w_to";
      case MappableDirection.NorthEast:
        return "ne_to";
      case MappableDirection.NorthWest:
        return "nw_to";
      case MappableDirection.SouthEast:
        return "se_to";
      case MappableDirection.SouthWest:
        return "sw_to";
      case MappableDirection.Up:
        return "u_to";
      case MappableDirection.Down:
        return "d_to";
      case MappableDirection.In:
        return "in_to";
      case MappableDirection.Out:
        return "out_to";
      default:
        Debug.Assert(false, "Unrecognized AutoMap direction.");
        return "north";
    }
  }

  private static string toI6String(string text, char quote) {
    if (text == null) text = string.Empty;
    return string.Format("{1}{0}{1}", text.Replace('\"', '~').Replace("\r", string.Empty).Replace('\n', '^'), quote);
  }

  private static string toI6Words(string text) {
    var words = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
    if (words.Length == 0) return toI6String("thing", SINGLE_QUOTE);
    var output = string.Empty;
    foreach (var word in words) {
      if (output.Length > 0) output += ' ';
      output += toI6String(DeAccent(word), SINGLE_QUOTE);
    }

    return output;
  }

  private void writeDoor(TextWriter writer, Location location, MappableDirection direction, Exit exit) {
    var oppositeDirection = CompassPointHelper.GetOpposite(direction);
    var reciprocal = exit.Target.GetBestExit(oppositeDirection);
    writer.WriteLine("Object {0} {1}", ExportName(exit.ConnectionName, null), exit.ConnectionDescription);
    writer.WriteLine("  with  name {0},", toI6Words(DeAccent(StripUnaccentedCharacters(exit.ConnectionName))));
    writer.WriteLine("        description {0},", toI6String(exit.ConnectionDescription, DOUBLE_QUOTE));
    writer.WriteLine("        found_in {0} {1},", location.ExportName, exit.Target.ExportName);
    writer.WriteLine("        door_to [; if (self in {0}) return {1}; return {0};],",
      location.ExportName,
      exit.Target.ExportName);
    writer.WriteLine("        door_dir [; if (self in {0}) return {1}; return {2}; ],",
      location.ExportName,
      toI6PropertyName(direction),
      toI6PropertyName(oppositeDirection));
    writer.WriteLine("  has   door {0} {1} {2} {3} ;",
      exit.Door.Openable ? "openable" : string.Empty,
      exit.Door.Open ? "open" : "~open",
      exit.Door.Lockable ? "lockable" : string.Empty,
      exit.Door.Locked ? "locked" : "~locked");
    reciprocal.Exported = true;
    writer.WriteLine();
  }

  private void writeDoors(TextWriter writer, Location location) {
    foreach (var direction in Directions.AllDirections) {
      var exit = location.GetBestExit(direction);
      if (exit?.Door == null || exit.Exported) continue;
      // remember we've exported this exit
      exit.Exported = true;
      writeDoor(writer, location, direction, exit);
    }
  }

  private void writeLocation(TextWriter writer, Location location) {
    writer.WriteLine("{0}  {1} {2}",
      location.Room.Region == Region.DefaultRegion ? "Object" : ExportName(location.Room.Region, null),
      location.ExportName,
      toI6String(location.Room.Name, DOUBLE_QUOTE));
    writeLocationDescription(writer, location);

    writeLocationDirections(writer, location);

    writeLocationLight(writer, location);
    writer.WriteLine();
  }

  private static void writeLocationDescription(TextWriter writer, Location location) {
    writer.WriteLine("  with  description");
    writer.WriteLine("            {0},", toI6String(location.Room.PrimaryDescription, DOUBLE_QUOTE));
  }

  private void writeLocationDirections(TextWriter writer, Location location) {
    foreach (var direction in Directions.AllDirections) {
      var exit = location.GetBestExit(direction);
      if (exit != null)
        writer.WriteLine("        {0} {1},",
          toI6PropertyName(direction),
          exit.Door != null ? ExportName(exit.ConnectionName, null) : exit.Target.ExportName);
    }
  }

  private static void writeLocationLight(TextWriter writer, Location location) {
    writer.WriteLine("   has  {0}light;", location.Room.IsDark ? "~" : string.Empty);
  }

  private static void writeOneThing(TextWriter writer, Thing thing, int indent, Thing container) {
    writer.WriteLine("Object {0} {1} {2}", "-> ".Repeat(indent),
      thing.ExportName,
      toI6String(StripUnaccentedCharacters(thing.DisplayName).Trim(), DOUBLE_QUOTE));

    writer.WriteLine("  with  name {0},", toI6Words(DeAccent(StripUnaccentedCharacters(thing.DisplayName))));
    writer.Write("        description {0}", toI6String(thing.DisplayName, DOUBLE_QUOTE));

    var attributes = setAttributes(thing);

    if (attributes.Count == 0) {
      writer.WriteLine(";");
    }
    else {
      writer.WriteLine();
      writer.WriteLine("  has " + string.Join(" ", attributes) + ";");
    }

    writer.WriteLine();
  }

  #endregion
}