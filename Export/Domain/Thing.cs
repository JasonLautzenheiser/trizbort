/*
    Copyright (c) 2010-2018 by Genstein and Jason Lautzenheiser.

    This file is (or was originally) part of Trizbort, the Interactive Fiction Mapper.

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Trizbort.Export.Domain; 

public sealed class Thing {
  public enum Amounts {
    Noforce,
    Singular,
    Plural
  }

  public enum ThingGender {
    Neuter,
    Male,
    Female
  }

  public Thing Container { get; }
  public List<Thing> Contents { get; }
  public string DisplayName { get; }
  public string ExportName { get; }
  public Amounts Forceplural { get; }
  public ThingGender Gender { get; }
  public int Indent { get; }
  public bool IsContainer { get; }
  public bool IsPerson { get; }
  public bool IsScenery { get; }
  public bool IsSupporter { get; }
  public Location Location { get; }
  public bool ProperNamed { get; }
  public bool Worn { get; }
  public bool PartOf { get; }
  public string PropString { get; }
  public string WarningText { get; }

  public Thing(string displayName, string exportName, Location location, Thing container, int indent,
               string propString) {
    DisplayName = displayName;
    ExportName = exportName;
    Location = location;
    Container = container;
    container?.Contents.Add(this);
    Indent = indent;
    WarningText = "";
    Contents = new List<Thing>();
    PropString = propString;


    var propRegx = new Regex("[fmp12csuwh!]");
    var errString = propRegx.Replace(PropString, "");

    if (!string.IsNullOrWhiteSpace(errString))
      WarningText += "The properties string " + PropString + " has the invalid character" +
                     (errString.Length == 1 ? "" : "s") + " " + errString + ".\n";

    //P defines a neuter person. F female, M male.
    if (propString.Contains("f")) {
      IsPerson = true;
      Gender = ThingGender.Female;
    }

    if (propString.Contains("m")) {
      if (IsPerson)
        WarningText += "You defined two different genders: " + Enum.GetName(typeof(ThingGender), Gender) +
                       " then male.\n";
      Gender = ThingGender.Male;
      IsPerson = true;
    }

    if (propString.Contains("p")) {
      if (IsPerson)
        WarningText += "You defined two different genders: " + Enum.GetName(typeof(ThingGender), Gender) +
                       " then neuter.\n";
      Gender = ThingGender.Neuter;
      IsPerson = true;
    }

    if (propString.Contains("w")) Worn = true;

    if (propString.Contains("h")) PartOf = true;

    //We can force plural or singular. Default is let Trizbort decide.
    Forceplural = Amounts.Noforce;
    if (propString.Contains("1")) Forceplural = Amounts.Singular;
    if (propString.Contains("2")) {
      if (Forceplural != Amounts.Noforce) WarningText += "You defined this object as both singular and plural.\n";
      Forceplural = Amounts.Plural;
    }

    //this isn't perfect. If something contains something else, then we need to add that as well.
    if (propString.Contains("c"))
      if (IsPerson)
        WarningText += "You defined this as a person and container. This will cause Inform to throw an error.\n";
      else
        IsContainer = true;
    if (propString.Contains("s")) {
      if (IsPerson)
        WarningText +=
          "You defined this as a person and scenery. Inform allows that, but you may not want to hide this person.\n";
      IsScenery = true;
    }

    if (propString.Contains("u"))
      if (IsPerson)
        WarningText += "You defined this as a person and a supporter. This will cause Inform to throw an error.\n";
      else
        IsSupporter = true;

    if (propString.Contains("!")) ProperNamed = true;
  }
}