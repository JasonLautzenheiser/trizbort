using Trizbort.Domain.Elements;

namespace Trizbort.Domain.Cache; 

public sealed class FindCacheItem {
  public string Description { get; set; }
  public Element Element { get; set; }
  public string Name { get; set; }
  public string Objects { get; set; }
  public string Subtitle { get; set; }

  public override string ToString() {
    return $"{Name} {Description} {Objects}".Trim();
  }
}