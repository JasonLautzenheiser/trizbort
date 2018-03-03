using System.IO;
using Trizbort.Setup;

namespace Trizbort.Domain.Application {
  public class MapLoader {
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

      return false;
    }

    private bool isEmptyFile(string fileName) {
      return new FileInfo(fileName).Length == 0;
    }
  }
}