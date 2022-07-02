using System;
using System.IO;
using System.Windows.Forms;
using Trizbort.Setup;

namespace Trizbort.Domain.Application; 

public sealed class MapLoader {
  private MapFileEngine loader;
  private Project project;

  public MapLoader(Project project) {
    this.project = project;
  }

  public bool LoadMap(string fileName) {
      
    // empty file
    if (isEmptyFile(fileName)) {
      Settings.Reset();
      return false;
    }

    if (Path.GetExtension(fileName) == ".trizbort") {
      loader = new LegacyMapFileEngine(project);
      return loader.Load(fileName);
    }

    MessageBox.Show($"'{fileName}' is not a known Trizbort file.", "Not a valid file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    return false;
  }

  private bool isEmptyFile(string fileName) {
    if (Uri.IsWellFormedUriString(fileName, UriKind.RelativeOrAbsolute))
      return false;

    return new FileInfo(fileName).Length == 0;
  }

  public bool LoadMap(Uri url) {

    return LoadMap(url.AbsoluteUri);
  }
}