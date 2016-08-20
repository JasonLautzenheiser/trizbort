namespace Trizbort.Domain.Cache
{
  public enum FindCacheObjectType
  {
    RoomName, 
    Connection,
  }

  public class FindCacheItem
  {
    public string Text { get; set; }
    public FindCacheObjectType Type { get; set; }

    public override string ToString()
    {
      return Text;
    }
  }
}