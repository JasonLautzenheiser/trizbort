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
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Misc;
using Trizbort.Domain.Watchers;
using Trizbort.Extensions;
using Trizbort.Setup;
using Trizbort.UI.Controls;
using Trizbort.Util;

namespace Trizbort.Domain.Application
{
  public class Project : IDisposable
  {
    public static readonly string FilterString = "Trizbort Map Files|*.trizbort";

    public static TrizbortFileWatcher FileWatcher = new TrizbortFileWatcher();
    private static Project mCurrent = new Project();
//    private MainForm mainForm;


    public Project()
    {
      Elements.Removed += onElementRemoved;
    }

    private void ReloadMap(object sender, EventArgs e)
    {
      TrizbortApplication.MainForm.OpenProject(FileName);
    }


    public static Project Current
    {
      get => mCurrent;
      set
      {
        if (mCurrent == value) return;
        var oldProject = mCurrent;
        mCurrent = value;
        raiseProjectChanged(oldProject, mCurrent);
      }
    }

    public Element ActiveSelectedElement { get; set; }

    public Canvas Canvas => TrizbortApplication.MainForm.Canvas;

    public bool IsDirty { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Description { get; set; }

    public string History { get; set; }

    public Version Version { get; set; }

    public BoundList<Element> Elements { get; } = new BoundList<Element>();

    public string FileName { get; set; } = string.Empty;

    public bool HasFileName => !string.IsNullOrEmpty(FileName);

    public bool MustHaveDescription { get; set; } = false;
    public bool MustHaveUniqueNames { get; set; } = false;
    public bool MustHaveSubtitle { get; set; } = false;
    public bool MustHaveNoDanglingConnectors { get; set; } = false;

    public string Name => !HasFileName ? "Untitled" : Path.GetFileNameWithoutExtension(FileName);

    public List<Element> GetSelectedElements()
    {
      return Canvas.SelectedElements.ToList();
    }

    /// <summary>
    ///   Handle element removal by removing elements which refer to it. May recurse.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void onElementRemoved(object sender, ItemEventArgs<Element> e)
    {
      var doomed = new List<Element>();
      foreach (var element in Elements.OfType<Connection>())
      {
        var connection = element;
        foreach (var vertex in connection.VertexList)
          if (vertex.Port != null && vertex.Port.Owner == e.Item)
            doomed.Add(element);
      }

      foreach (var element in doomed)
        Elements.Remove(element);
    }

    public static event ProjectChangedEventHandler ProjectChanged;

    private static void raiseProjectChanged(Project oldProject, Project newProject)
    {
      var projectChanged = ProjectChanged;
      projectChanged?.Invoke(null, new ProjectChangedEventArgs(oldProject, newProject));
    }

    /// <summary>
    ///   Test whether the given identifier is in use.
    /// </summary>
    /// <param name="id">The identifier to test.</param>
    /// <returns>True if an element is using this identifier; false otherwise.</returns>
    public bool IsElementIDInUse(int id)
    {
      Element element;
      return FindElement(id, out element);
    }

    /// <summary>
    ///   Find the element with the given ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public bool FindElement(int id, out Element element)
    {
      foreach (var existing in Elements.Where(existing => existing.ID == id))
      {
        element = existing;
        return true;
      }
      element = null;
      return false;
    }

    public List<Element> GetElementByName(string name)
    {
      var list = Elements.Where(p => p.Name == name).ToList();
      return list;
    }

    public bool Load()
    {
      FileWatcher.ReloadMap -= ReloadMap;

      var loader = new MapLoader(this);
      return loader.LoadMap(FileName);
    }

    public void CheckDocVersion()
    {
      var appVers = Version.Parse(System.Windows.Forms.Application.ProductVersion);
      var infoList = $"Executable Version = {System.Windows.Forms.Application.ProductVersion}{Environment.NewLine}Document Version = {Version}{Environment.NewLine}{Environment.NewLine}";
      var newVersionText = $"Visit www.trizbort.com to learn about and download the latest version.";

      if (Version.Major < appVers.Major) return;
      if (Version.Major > appVers.Major)
      {
        MessageBox.Show(Program.MainForm, $"{infoList}The document is ahead a major version. Information is very likely to be lost.{Environment.NewLine}{Environment.NewLine}{newVersionText}", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      if (Version.Minor < appVers.Minor) return;
      if (Version.Minor > appVers.Minor)
      {
        MessageBox.Show(Program.MainForm, $"{infoList}The document is ahead a minor version. Information is likely to be lost.{Environment.NewLine}{Environment.NewLine}{newVersionText}", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      if (Version.Build < appVers.Build) return;
      if (Version.Build > appVers.Build)
      {
        MessageBox.Show(Program.MainForm, $"{infoList}The document is ahead a build. Information is somewhat likely to be lost.{Environment.NewLine}{Environment.NewLine}{newVersionText}", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      if (Version.MinorRevision < appVers.MinorRevision) return;
      if (Version.MinorRevision > appVers.MinorRevision)
      {
        MessageBox.Show(Program.MainForm, $"{infoList}The document is ahead a minor revision. Information may possibly be lost.{Environment.NewLine}{Environment.NewLine}{newVersionText}", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    public void SetVersion(string versionNumber)
    {
      try { Version = Version.Parse(versionNumber); }
      catch (Exception)
      {
        Version = new Version(0, 0, 0, 0);
      }
    }

    public bool Backup()
    {
      if (HasFileName)
      {
        var nextAvailableFilename = FileName.NextAvailableFilename();
        File.Copy(FileName, nextAvailableFilename);
        MessageBox.Show($"You project has been backed up to {nextAvailableFilename}.", "Project backed up.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return true;
      }
      MessageBox.Show("Your project has not yet been saved to a file. There is nothing to backup.", "Nothing to backup.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return false;
    }

    public bool Save()
    {
      FileWatcher.StopWatcher();

      var settings = new XmlWriterSettings {Encoding = Encoding.UTF8, Indent = true, IndentChars = "\t"};

      try
      {
        using (var scribe = XmlScribe.Create(FileName))
        {
          scribe.StartElement("trizbort");
          scribe.Attribute("version", System.Windows.Forms.Application.ProductVersion);
          scribe.StartElement("info");
          if (!string.IsNullOrEmpty(Title))
            scribe.Element("title", Title);
          if (!string.IsNullOrEmpty(Author))
            scribe.Element("author", Author);
          if (!string.IsNullOrEmpty(Description))
            scribe.Element("description", Description);
          if (!string.IsNullOrEmpty(History))
            scribe.Element("history", History);
          scribe.EndElement();
          scribe.StartElement("map");
          foreach (var element in Elements)
            saveElement(scribe, element);
          scribe.EndElement();
          scribe.StartElement("settings");
          Settings.Save(scribe);
          scribe.EndElement();
        }
        IsDirty = false;
        FileWatcher.StartWatcher();
        return true;
      }
      catch (Exception ex)
      {
        MessageBox.Show(Program.MainForm, string.Format("There was a problem saving the map:\n\n{0}", ex.Message), System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        FileWatcher.StartWatcher();
        return false;
      }
    }

    private static void saveElement(XmlScribe scribe, Element element)
    {
      if (element.GetType() == typeof(Room))
      {
        scribe.StartElement("room");
        scribe.Attribute("id", element.ID);
        ((Room) element).Save(scribe);
        scribe.EndElement();
      }
      else if (element.GetType() == typeof(Connection))
      {
        scribe.StartElement("line");
        scribe.Attribute("id", element.ID);
        ((Connection) element).Save(scribe);
        scribe.EndElement();
      }
    }

    public bool AreRoomsConnected(List<Room> selectedRooms)
    {
      if (selectedRooms.Count < 2) return false;
      if (selectedRooms.Count == 2)
      {
        var room1 = selectedRooms.First();
        var room2 = selectedRooms.Last();

        if (room1.IsConnected && room2.IsConnected)
        {
          var con = room1.GetConnections();
          foreach (var connection in con)
          {
            if (connection.GetSourceRoom() == room1)
              if (connection.GetTargetRoom() == room2)
                return true;

            if (connection.GetTargetRoom() == room1)
              if (connection.GetSourceRoom() == room2)
                return true;
          }
        }

        return false;
      }
      return false;
    }

    public void Dispose()
    {
      FileWatcher.ReloadMap -= ReloadMap;
    }

    public void InitFileWWatcher(string fileName) {
      FileWatcher.ReloadMap += ReloadMap;
      FileWatcher.InitializeWatcher(fileName);
    }
  }
}