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
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Text;
using System.Windows.Forms;

namespace Trizbort
{
    internal class Project
    {
        public Project()
        {
            Elements.Removed += OnElementRemoved;
        }

        /// <summary>
        /// Handle element removal by removing elements which refer to it. May recurse.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        void OnElementRemoved(object sender, ItemEventArgs<Element> e)
        {
            var doomed = new List<Element>();
            foreach (var element in Elements)
            {
                if (element is Connection)
                {
                    var connection = (Connection)element;
                    foreach (var vertex in connection.VertexList)
                    {
                        if (vertex.Port != null && vertex.Port.Owner == e.Item)
                        {
                            doomed.Add(element);
                        }
                    }
                }
            }

            foreach (var element in doomed)
            {
                Elements.Remove(element);
            }
        }

        public static Project Current
        {
            get { return m_current; }
            set
            {
                if (m_current != value)
                {
                    var oldProject = m_current;
                    m_current = value;
                    RaiseProjectChanged(oldProject, m_current);
                }
            }
        }

        public static event ProjectChangedEventHandler ProjectChanged;

        private static void RaiseProjectChanged(Project oldProject, Project newProject)
        {
            var projectChanged = ProjectChanged;
            if (projectChanged != null)
            {
                projectChanged(null, new ProjectChangedEventArgs(oldProject, newProject));
            }
        }

        public bool IsDirty
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string History
        {
            get;
            set;
        }

      public Version Version { get; set; }

        public BoundList<Element> Elements
        {
            get { return m_elements; }
        }

        /// <summary>
        /// Test whether the given identifier is in use.
        /// </summary>
        /// <param name="id">The identifier to test.</param>
        /// <returns>True if an element is using this identifier; false otherwise.</returns>
        public bool IsElementIDInUse(int id)
        {
            Element element;
            return FindElement(id, out element);
        }

        /// <summary>
        /// Find the element with the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool FindElement(int id, out Element element)
        {
            foreach (var existing in Elements)
            {
                if (existing.ID == id)
                {
                    element = existing;
                    return true;
                }
            }
            element = null;
            return false;
        }

        public string FileName
        {
            get { return m_fileName; }
            set
            {
                if (m_fileName != value)
                {
                    m_fileName = value;
                }
            }
        }

        public bool HasFileName
        {
            get { return !string.IsNullOrEmpty(m_fileName); }
        }

        public string Name
        {
            get
            {
                if (!HasFileName)
                {
                    return "Untitled";
                }

                return Path.GetFileNameWithoutExtension(m_fileName);
            }
        }

        public bool Load()
        {
            try
            {
                if (new FileInfo(FileName).Length == 0)
                {
                    // this is an empty file, probably thanks to our Explorer New->Trizbort Map menu option.
                    Settings.Reset();
                    return true;
                }

                var doc = new XmlDocument();
                doc.Load(FileName);
                var root = new XmlElementReader(doc.DocumentElement);

                if (!root.HasName("trizbort"))
                    throw new InvalidDataException(string.Format("Not a {0} map file.", Application.ProductName));

              // file version
               var versionNumber = root.Attribute("version").Text;
              setVersion(versionNumber);
  
              // load info
                Title = root["info"]["title"].Text;
                Author = root["info"]["author"].Text;
                Description = root["info"]["description"].Text;
                History = root["info"]["history"].Text;

                // load all elements
                var map = root["map"];
                var mapConnectionToLoadState = new Dictionary<Connection, object>();
                foreach (var element in map.Children)
                {
                    if (element.HasName("room"))
                    {
                        // Changed the constructor used for elements when loading a file for a significant speed increase
                        var room = new Room(this, Elements.Count+1);
                        room.ID = element.Attribute("id").ToInt(room.ID);
                        room.Load(element);
                        Elements.Add(room);
                    }
                    else if (element.HasName("line"))
                    {
                        // Changed the constructor used for elements when loading a file for a significant speed increase
                        var connection = new Connection(this, Elements.Count+1);
                        connection.ID = element.Attribute("id").ToInt(connection.ID);
                        var loadState = connection.BeginLoad(element);
                        if (loadState != null)
                        {
                            mapConnectionToLoadState.Add(connection, loadState);
                        }
                        Elements.Add(connection);
                    }
                }

                // connect them together
                foreach (var pair in mapConnectionToLoadState)
                {
                    var connection = pair.Key;
                    var state = pair.Value;
                    connection.EndLoad(state);
                }

                // load settings last, since their load can't be undone
                Settings.Reset();
                Settings.Load(root["settings"]);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.MainForm, string.Format("There was a problem loading the map:\n\n{0}", ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

      private void setVersion(string versionNumber)
      {
        try { Version = Version.Parse(versionNumber); }
        catch (Exception)
        {
          Version = new Version(0,0,0,0);
        }
      }

      public bool Save()
        {
          var settings = new XmlWriterSettings {Encoding = Encoding.UTF8, Indent = true, IndentChars = "\t"};

          try
            {
                using (var scribe = XmlScribe.Create(FileName))
                {
                    scribe.StartElement("trizbort");
                    scribe.Attribute("version", Application.ProductVersion);
                    scribe.StartElement("info");
                    if (!string.IsNullOrEmpty(Title))
                    {
                        scribe.Element("title", Title);
                    }
                    if (!string.IsNullOrEmpty(Author))
                    {
                        scribe.Element("author", Author);
                    }
                    if (!string.IsNullOrEmpty(Description))
                    {
                        scribe.Element("description", Description);
                    }
                    if (!string.IsNullOrEmpty(History))
                    {
                        scribe.Element("history", History);
                    }
                    scribe.EndElement();
                    scribe.StartElement("map");
                    foreach (var element in Elements)
                    {
                        SaveElement(scribe, element);
                    }
                    scribe.EndElement();
                    scribe.StartElement("settings");
                    Settings.Save(scribe);
                    scribe.EndElement();
                }
                IsDirty = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.MainForm, string.Format("There was a problem saving the map:\n\n{0}", ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void SaveElement(XmlScribe scribe, Element element)
        {
            if (element is Room)
            {
                scribe.StartElement("room");
                scribe.Attribute("id", element.ID);
                ((Room)element).Save(scribe);
                scribe.EndElement();
            }
            else if (element is Connection)
            {
                scribe.StartElement("line");
                scribe.Attribute("id", element.ID);
                ((Connection)element).Save(scribe);
                scribe.EndElement();
            }
        }

        public static readonly string FilterString = "Trizbort Map Files|*.trizbort";

        private static Project m_current = new Project();
        private string m_fileName = string.Empty;
        private BoundList<Element> m_elements = new BoundList<Element>();
    }
}
