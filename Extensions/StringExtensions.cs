using System;

namespace Trizbort.Extensions {
  public static class StringExtensions {
    public static bool StartsWithVowel(this string c) {
      if (string.IsNullOrWhiteSpace(c)) return false;

      var firstChar = c.Substring(0, 1);

      var isVowel = "aeiou".IndexOf(firstChar, StringComparison.InvariantCultureIgnoreCase) >= 0;
      return isVowel;
    }

    public static bool isUrl(this string c) {
      return Uri.IsWellFormedUriString(c, UriKind.RelativeOrAbsolute);
    }
  }
}