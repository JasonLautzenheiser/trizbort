using NUnit.Framework;
using Shouldly;
using Trizbort.Extensions;

namespace Trizbort.Tests
{

  [TestFixture]
  public class StringExentensionTests
  {
    [Test]
    public void StartsWithVowel_EmptyString_ReturnsFalse()
    {
      "".StartsWithVowel().ShouldBeFalse();
    }

    [Test]
    public void StartsWithVowel_Null_ReturnsFalse()
    {
      ((string)null).StartsWithVowel().ShouldBeFalse();
    }

    [Test]
    public void StartsWithVowel_StartsWithConsonant_ReturnsFalse()
    {
      "trizbort".StartsWithVowel().ShouldBeFalse();
    }

    [Test]
    public void StartsWithVowel_StartsWithVowel_ReturnsFalse()
    {
      "apple".StartsWithVowel().ShouldBeTrue();
    }

  }
}
