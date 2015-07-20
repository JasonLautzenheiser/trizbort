using NUnit.Framework;
using Trizbort.Extensions;

namespace Trizbort.Tests
{

  [TestFixture]
  public class StringExentensionTests
  {
    [Test]
    public void StartsWithVowel_EmptyString_ReturnsFalse()
    {
      Assert.AreEqual(false, "".StartsWithVowel());
    }

    [Test]
    public void StartsWithVowel_Null_ReturnsFalse()
    {
      Assert.AreEqual(false, ((string)null).StartsWithVowel());
    }

    [Test]
    public void StartsWithVowel_StartsWithConsonant_ReturnsFalse()
    {
      Assert.AreEqual(false, "trizbort".StartsWithVowel());
    }

    [Test]
    public void StartsWithVowel_StartsWithVowel_ReturnsFalse()
    {
      Assert.AreEqual(true, "apple".StartsWithVowel());
    }

  }
}
