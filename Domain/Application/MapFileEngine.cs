namespace Trizbort.Domain.Application {
  public abstract class MapFileEngine {
    private string fileName;
    protected MapFileEngine() { }

    protected MapFileEngine(string fileName) {
      this.fileName = fileName;
    }

    public virtual bool Load() {
      return Load(fileName);
    }

    public virtual bool Load(string fileName) {
      return true;
    }
  }
}