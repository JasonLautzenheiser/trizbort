using Trizbort.Domain.Elements;

namespace Trizbort.Domain.Cache
{
  public enum FindCacheObjectType
  {
    RoomName, 
    Subtitle,
    RoomDescription,
    Objects,
  }

  public class FindCacheItem
  {
    public Element Element { get; set; }
    public string Name { get; set; }
    public string Subtitle { get; set; }
    public string Description { get; set; }
    public string Objects { get; set; }

    public override string ToString()
    {
      return $"{Name} {Description} {Objects}".Trim();
    }
  }
}