using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Trizbort.Automap.Utility
{
  public class PeekingStreamReader : StreamReader
  {
    private readonly Queue<string> peeks;
    public PeekingStreamReader(Stream stream) : base(stream)
    {
      peeks = new Queue<string>();
    }

    public override Task<string> ReadLineAsync()
    {
      return Task.Run<string>(() => ReadLine());
    }

    public override string ReadLine()
    {
      if (peeks.Count <= 0) return base.ReadLine();

      var nextLine = peeks.Dequeue();
      return nextLine;
    }

    public string PeekReadLine()
    {
      var nextLine = base.ReadLine();
      peeks.Enqueue(nextLine);
      return nextLine;
    }
  }
}