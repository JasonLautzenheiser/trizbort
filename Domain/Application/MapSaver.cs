using System.IO;

namespace Trizbort.Domain.Application; 

public sealed class MapSaver {
  private readonly Project project;

  public MapSaver(Project project) {
    this.project = project;
  }

  public bool SaveMap(string fileName) {
    if (Path.GetExtension(fileName) != ".trizbort") return false;
    MapFileEngine engine = new LegacyMapFileEngine(project);
    return engine.Save(fileName);
  }
}