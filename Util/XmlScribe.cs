/*
    Copyright (c) 2010-2015 by Genstein and Jason Lautzenheiser.

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
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Trizbort.Util
{
    public class XmlScribe : IDisposable
    {
        private XmlScribe(XmlWriter writer)
        {
            Writer = writer;
            Writer.WriteStartDocument();
        }

        public static XmlScribe Create(string fileName)
        {
            var settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.IndentChars = "\t";

            return new XmlScribe(XmlWriter.Create(fileName, settings));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            Writer.WriteEndDocument();
            Writer.Close();
        }

        public XmlWriter Writer
        {
            get; private set;
        }

        public void StartElement(string localName)
        {
            Writer.WriteStartElement(localName);
        }

        public void EndElement()
        {
            Writer.WriteEndElement();
        }

        public void Element(string localName, string value)
        {
            Writer.WriteElementString(localName, value ?? string.Empty);
        }

        public void Element(string localName, float value)
        {
            Writer.WriteElementString(localName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Element(string localName, bool value)
        {
            Writer.WriteElementString(localName, value ? Yes : No);
        }

        public void Element(string localName, Color value)
        {
            Writer.WriteElementString(localName, ColorTranslator.ToHtml(value));
        }

        public void Element(string localName, CompassPoint value)
        {
            string name;
            if (CompassPointHelper.ToName(value, out name))
            {
                Writer.WriteElementString(localName, name);
            }
        }

        public void Attribute(string localName, string value)
        {
            Writer.WriteAttributeString(localName, value);
        }

        public void Attribute(string localName, float value)
        {
            Writer.WriteAttributeString(localName, value.ToString(CultureInfo.InvariantCulture));
        }

        public void Attribute(string localName, bool value)
        {
            Writer.WriteAttributeString(localName, value ? Yes : No);
        }

        public void Attribute(string localName, Color value)
        {
            Writer.WriteAttributeString(localName, ColorTranslator.ToHtml(value));
        }

        public void Attribute(string localName, CompassPoint value)
        {
            string name;
            if (CompassPointHelper.ToName(value, out name))
            {
                Writer.WriteAttributeString(localName, name);
            }
        }

        public void Value(string value)
        {
            Writer.WriteValue(value);
        }

        public void Value(float value)
        {
            Writer.WriteValue(value);
        }

        public void Value(bool value)
        {
            Writer.WriteValue(value ? Yes : No);
        }

        public void Value(Color value)
        {
            Writer.WriteValue(ColorTranslator.ToHtml(value));
        }

        public void Value(CompassPoint value)
        {
            string name;
            if (CompassPointHelper.ToName(value, out name))
            {
                Writer.WriteValue(name);
            }
        }

        public static readonly string Yes = "yes";
        public static readonly string No = "no";
    }
}
