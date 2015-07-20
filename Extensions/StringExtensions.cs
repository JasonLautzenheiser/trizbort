using System;

namespace Trizbort.Extensions
{
  public static class StringExtensions
  {
    public static bool StartsWithVowel(this string c )
    {
      if (string.IsNullOrWhiteSpace(c)) return false;
      var isVowel = "aeiou".IndexOf(c, StringComparison.InvariantCultureIgnoreCase) >= 0;
      return isVowel;
    }
  }
}