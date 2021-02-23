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
using Trizbort.Util;

namespace Trizbort.Domain.Misc
{
  public class CustomAttributeDefinition : IEquatable<CustomAttributeDefinition>
  {
    public virtual string Name { get; set; }

    public virtual string DataType { get; set; } = "String"; // Future proofing, not actively used currently

    public virtual string ObjectType { get; set;  } // "room" or "connection"

    public virtual void Save(XmlScribe scribe)
    {
      scribe.StartElement("attributeDefinition");

      scribe.Attribute("name", Name);
      scribe.Attribute("dataType", DataType);
      scribe.Attribute("objectType", ObjectType);

      scribe.EndElement();
    }

    public virtual void Load(XmlElementReader element)
    {
      Name = element.Attribute("name").Text;
      DataType = element.Attribute("dataType").Text;
      ObjectType = element.Attribute("objectType").Text;
    }

    public virtual string ToExportFileLine()
    {
      return $"{Name}|{DataType}|{ObjectType}";
    }

    public int GetHashCode(object obj)
    {
      var hash = 17;
      hash *= 23 + Name.GetHashCode();
      hash *= 23 + DataType.GetHashCode();
      hash *= 23 + ObjectType.GetHashCode();
      return hash;
    }

    public bool Equals(CustomAttributeDefinition other)
    {
      return Name == other.Name && DataType == other.DataType && ObjectType == other.ObjectType;
    }

    public static CustomAttributeDefinition FromImportFileLine(string line)
    {
      var fields = line.Split('|');

      if (fields.Length != 3)
      {
        return null;
      }

      var newDef = new CustomAttributeDefinition
      {
        Name = fields[0],
        DataType = fields[1],
        ObjectType = fields[2],
      };

      if (newDef.DataType != "String")
      {
        return null;
      }

      if (newDef.ObjectType != "room" && newDef.ObjectType != "connection")
      {
        return null;
      }

      return newDef;
    }
  }
}
