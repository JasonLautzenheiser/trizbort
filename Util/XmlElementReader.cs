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
using System.Drawing;
using System.Globalization;
using System.Xml;
using Trizbort.Domain.Misc;

namespace Trizbort.Util; 

/// <summary>
/// Wrapper around an XmlElement for ease of access.
/// </summary>
public class XmlElementReader
{
  public XmlElementReader()
  {
  }

  public XmlElementReader(XmlElement element)
  {
    Element = element;
  }

  public XmlElementReader this[string localName]
  {
    get
    {
      if (Element != null)
      {
        foreach (var node in Element.ChildNodes)
        {
          if (!(node is XmlElement))
            continue;

          var element = (XmlElement)node;
          if (element.LocalName == localName)
          {
            return new XmlElementReader(element);
          }
        }
      }
      return new XmlElementReader();
    }

  }

  public List<XmlElementReader> Children
  {
    get
    {
      if (m_children == null)
      {
        m_children = new List<XmlElementReader>();
        if (Element != null)
        {
          foreach (var node in Element.ChildNodes)
          {
            if (!(node is XmlElement))
              continue;

            var element = (XmlElement)node;
            m_children.Add(new XmlElementReader(element));
          }
        }
      }
      return m_children;
    }
  }

  public string Name
  {
    get
    {
      if (Element != null)
      {
        return Element.LocalName;
      }
      return string.Empty;
    }
  }

  public bool HasName(string value)
  {
    return StringComparer.InvariantCultureIgnoreCase.Compare(Name, value) == 0;
  }

  public string Text
  {
    get
    {
      if (Element != null)
      {
        return Element.InnerText;
      }
      return string.Empty;
    }
  }

  public string ToText(string defaultValue)
  {
    var text = Text;
    if (!string.IsNullOrEmpty(text))
    {
      return text;
    }
    return defaultValue;
  }

  public int ToInt()
  {
    return ToInt(0);
  }

  public int ToInt(int defaultValue)
  {
    int value;
    if (int.TryParse(Text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
    {
      return value;
    }
    return defaultValue;
  }

  public float ToFloat()
  {
    return ToFloat(0);
  }

  public float ToFloat(float defaultValue)
  {
    float value;
    if (float.TryParse(Text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
    {
      return value;
    }
    return defaultValue;
  }

  public bool ToBool()
  {
    return ToBool(false);
  }

  public bool ToBool(bool defaultValue)
  {
    if (StringComparer.InvariantCultureIgnoreCase.Compare(Text, XmlScribe.Yes) == 0)
    {
      return true;
    }
    else if (StringComparer.InvariantCultureIgnoreCase.Compare(Text, XmlScribe.No) == 0)
    {
      return false;
    }
    return defaultValue;
  }

  public Color ToColor(Color defaultValue)
  {
    try
    {
      return ColorTranslator.FromHtml(Text);
    }
    catch (Exception)
    {
      return defaultValue;
    }
  }

  public CompassPoint ToCompassPoint(CompassPoint defaultValue)
  {
    CompassPoint point;
    if (CompassPointHelper.FromName(Text, out point))
    {
      return point;
    }
    return defaultValue;
  }

  public XmlAttributeReader Attribute(string localName)
  {
    if (Element != null)
    {
      foreach (XmlAttribute attribute in Element.Attributes)
      {
        if (StringComparer.InvariantCultureIgnoreCase.Compare(attribute.LocalName, localName) == 0)
        {
          return new XmlAttributeReader(attribute.Value);
        }
      }
    }
    return new XmlAttributeReader(string.Empty);
  }

  public XmlElement Element
  {
    get;
    private set;
  }

  private List<XmlElementReader> m_children;
}