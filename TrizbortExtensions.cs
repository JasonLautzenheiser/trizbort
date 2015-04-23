using System;
using System.IO;

namespace TrizbortExtensions
{
    // TODO: Test this code

    public static class StreamReaderExtension
    {
        public static long GetPosition(this StreamReader reader)
        {
            reader.BaseStream.Flush();
            return reader.BaseStream.Position;
        }

        public static void SetPosition(this StreamReader reader, long position)
        {
            reader.BaseStream.Position = position;
            reader.DiscardBufferedData();
        }
    }
}
