using System;
using NUnit.Framework;
using Server;

namespace Tests
{
    [TestFixture]
    public class InsensitiveTests
    {
        [Test]
        public void Equals_BothNull_ReturnsTrue()
        {
            Assert.IsTrue(Insensitive.Equals(null, null));
        }

        [Test]
        public void Equals_OneNull_ReturnsFalse()
        {
            Assert.IsFalse(Insensitive.Equals("a", null));
            Assert.IsFalse(Insensitive.Equals(null, "b"));
        }

        [Test]
        public void Equals_DifferentLengths_ReturnsFalse()
        {
            Assert.IsFalse(Insensitive.Equals("abc", "ab"));
        }

        [Test]
        public void Equals_SameString_ReturnsTrue()
        {
            Assert.IsTrue(Insensitive.Equals("hello", "hello"));
        }

        [Test]
        public void Equals_DifferentCase_ReturnsTrue()
        {
            Assert.IsTrue(Insensitive.Equals("HELLO", "hello"));
            Assert.IsTrue(Insensitive.Equals("HeLlO", "hElLo"));
        }

        [Test]
        public void Equals_DifferentStrings_ReturnsFalse()
        {
            Assert.IsFalse(Insensitive.Equals("hello", "world"));
        }
    }
}
