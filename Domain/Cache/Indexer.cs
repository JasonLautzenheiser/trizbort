using System.Collections.Generic;
using System.Linq;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;

namespace Trizbort.Domain.Cache {
  public class Indexer {
    public List<FindCacheItem> Index() {
      return Project.Current.Elements.OfType<Room>()
                    .Select(room => new FindCacheItem {
                      Element = room,
                      Name = room.Name,
                      Description = room.PrimaryDescription,
                      Objects = room.Objects,
                      Subtitle = room.SubTitle
                    })
                    .ToList();
    }
  }
}