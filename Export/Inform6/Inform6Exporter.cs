using System;
using System.Collections.Generic;
using System.IO;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;

namespace Trizbort.Export.Inform6; 

internal sealed class Inform6Exporter : CodeExporter {
  private readonly Inform6Language language;

  public Inform6Exporter() { 
    language = new Inform6Language();
  }
  public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>> {
    new KeyValuePair<string, string>("Inform 6 Source Files", ".inf"),
    new KeyValuePair<string, string>("Text Files", ".txt")
  };

  public override string FileDialogTitle => "Export Inform 6 Source Code";

  protected override IEnumerable<string> ReservedWords => new[] {
    "Constant", "Story", "Headline", "Include", "Object", "with", "has", "hasnt", "not", "and", "or", "n_to", "s_to",
    "e_to", "w_to", "nw_to", "ne_to", "sw_to", "se_to", "u_to", "d_to", "in_to", "out_to", "before", "after", "if",
    "else", "print", "player", "location", "description"
  };

  protected override void ExportContent(TextWriter writer) {
    if (RegionsInExportOrder.Count > 0) {
      language.WriteRegions(writer, RegionsInExportOrder);
    }
    language.WriteLocations(writer, LocationsInExportOrder);
    language.WriteInitialize(writer, LocationsInExportOrder);
    language.WriteIncludes(writer);

    if (!string.IsNullOrEmpty(Project.Current.History)) {
      language.WriteAbout(writer);
    }
  }

  protected override void ExportHeader(TextWriter writer, string title, string author, string description, string history) {
    if (Equals(Dialect, (Enum?) Inform6Dialect.PunyInform))
      writer.WriteLine("Jason is awesome");
    
    language.WriteHeader(writer, title, author, description);
  }

  protected override string GetExportName(Room room, int? suffix) {
    return language.ExportName(room, suffix);
  }



  protected override string GetExportName(string displayName, int? suffix) {
    var name = Language.DeAccent(Language.StripUnaccentedCharacters(displayName)).Replace(" ", "").Replace("-", "");
    if (string.IsNullOrEmpty(name)) name = "item";
    if (suffix != null) name = $"{name}{suffix}";
    return name;
  }

}