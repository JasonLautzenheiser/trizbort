using System.Collections.Generic;

namespace Trizbort.Domain.Cache
{
  public class Indexer
  {
    public List<FindCacheItem> Index()
    {
      var items = new List<FindCacheItem>();

      foreach (var element in Project.Current.Elements)
      {

        if (!(element is Room || element is Connection)) continue;

        var cacheItem = new FindCacheItem();
        if (element is Room)
        {
          var room = (Room) element;
          if (string.IsNullOrWhiteSpace(room.Name)) continue;
          cacheItem.Text = room.Name;
          cacheItem.Type = FindCacheObjectType.RoomName;
        }

        if (element is Connection)
        {
          var connection = (Connection)element;
          if (string.IsNullOrWhiteSpace(connection.Name)) continue;
          cacheItem.Text = connection.Name;
          cacheItem.Type = FindCacheObjectType.Connection;
        }

        items.Add(cacheItem);
      }

      return items;
    }
  }
}