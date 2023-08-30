using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Trizbort.Domain.Elements;
using Trizbort.Extensions;
using Trizbort.Setup;
using Trizbort.Util;

namespace Trizbort.Domain.Application {
  public class LegacyMapFileEngine : MapFileEngine
  {
    private Project project;

    public LegacyMapFileEngine(Project project)
    {
      this.project = project;
    }

    public override bool Load(string fileName)
    {
      try
      {
        if (!fileName.IsUrl() && new FileInfo(fileName).Length == 0)
        {
          // this is an empty file, probably thanks to our Explorer New->Trizbort Map menu option.
          Settings.Reset();
          return true;
        }

        var doc = new XmlDocument();
        doc.Load(fileName);
        var root = new XmlElementReader(doc.DocumentElement);

        if (!root.HasName("trizbort"))
          throw new InvalidDataException($"Not a {System.Windows.Forms.Application.ProductName} map file.");

        //reset checks: we may make this into a function if we ever wish to verify a Trizbort file first.
        Settings.StartRoomLoaded = false;
        Settings.EndRoomLoaded = false;

        // file version
        var versionNumber = root.Attribute("version").Text;
        project.SetVersion(versionNumber);
        project.CheckDocVersion();

        // load info
        project.Title = root["info"]["title"].Text;
        project.Author = root["info"]["author"].Text;
        project.Description = root["info"]["description"].Text;
        project.History = root["info"]["history"].Text;

        // load all elements
        var map = root["map"];
        var mapConnectionToLoadState = new Dictionary<Connection, object>();
        foreach (var element in map.Children)
          if (element.HasName("room"))
          {
            // Changed the constructor used for elements when loading a file for a significant speed increase
            var room = new Room(project, project.Elements.Count + 1);
            room.ID = element.Attribute("id").ToInt(room.ID);
            room.Load(element);
            project.Elements.Add(room);
          }
          else if (element.HasName("line"))
          {
            // Changed the constructor used for elements when loading a file for a significant speed increase
            var connection = new Connection(project, project.Elements.Count + 1);
            connection.ID = element.Attribute("id").ToInt(connection.ID);
            var loadState = connection.BeginLoad(element);
            if (loadState != null)
              mapConnectionToLoadState.Add(connection, loadState);
            project.Elements.Add(connection);
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

        // setup filewatcher.
        if (!fileName.IsUrl())
          project.InitFileWWatcher(fileName);

        return true;
      }
      catch (Exception ex)
      {
        MessageBox.Show(Program.MainForm, $"There was a problem loading the map:{Environment.NewLine}{Environment.NewLine}{ex.Message}", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

    }

    public override bool Save(string fileName) {
      try {
        using (var scribe = XmlScribe.Create(fileName)) {
          scribe.StartElement("trizbort");
          scribe.Attribute("version", Assembly.GetEntryAssembly().GetName().Version.ToString());
          scribe.StartElement("info");
          if (!string.IsNullOrEmpty(project.Title))
            scribe.Element("title", project.Title);
          if (!string.IsNullOrEmpty(project.Author))
            scribe.Element("author", project.Author);
          if (!string.IsNullOrEmpty(project.Description))
            scribe.Element("description", project.Description);
          if (!string.IsNullOrEmpty(project.History))
            scribe.Element("history", project.History);
          scribe.EndElement();
          scribe.StartElement("map");
          foreach (var element in project.Elements)
            saveElement(scribe, element);
          scribe.EndElement();
          scribe.StartElement("settings");
          Settings.Save(scribe);
          scribe.EndElement();
        }
        return true;
      }
      catch (Exception) {
        throw;
      }
    }

    private void saveElement(XmlScribe scribe, Element element)
    {
      if (element.GetType() == typeof(Room))
      {
        scribe.StartElement("room");
        scribe.Attribute("id", element.ID);
        ((Room)element).Save(scribe);
        scribe.EndElement();
      }
      else if (element.GetType() == typeof(Connection))
      {
        scribe.StartElement("line");
        scribe.Attribute("id", element.ID);
        ((Connection)element).Save(scribe);
        scribe.EndElement();
      }
    }
  }
}