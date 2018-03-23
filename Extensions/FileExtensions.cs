using System;
using System.IO;

namespace Trizbort.Extensions {
  public static class FileExtensions {
    private static readonly string numberPattern = "-backup-{0}";

    public static string NextAvailableFilename(this string path) {
      if (!File.Exists(path))
        return path;

      if (Path.HasExtension(path))
        return getNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path), StringComparison.CurrentCultureIgnoreCase), numberPattern));

      return getNextFilename(path + numberPattern);
    }

    private static string getNextFilename(string pattern) {
      var tmp = string.Format(pattern, 1);
      if (tmp == pattern)
        throw new ArgumentException("The pattern must include an index place-holder", nameof(pattern));

      if (!File.Exists(tmp))
        return tmp; // short-circuit if no matches

      int min = 1, max = 2; // min is inclusive, max is exclusive/untested

      while (File.Exists(string.Format(pattern, max))) {
        min = max;
        max *= 2;
      }

      while (max != min + 1) {
        var pivot = (max + min) / 2;
        if (File.Exists(string.Format(pattern, pivot)))
          min = pivot;
        else
          max = pivot;
      }

      return string.Format(pattern, max);
    }
  }
}