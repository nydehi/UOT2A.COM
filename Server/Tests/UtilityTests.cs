using System;
using NUnit.Framework;

namespace Server.Tests
{
    [TestFixture]
    public class UtilityTests
    {
        [Test]
        public void FixHtml_NullString_ReturnsEmptyString()
        {
            string result = Utility.FixHtml(null);
            Assert.AreEqual("", result);
        }

        [Test]
        public void FixHtml_NoSpecialCharacters_ReturnsSameString()
        {
            string input = "Hello World";
            string result = Utility.FixHtml(input);
            Assert.AreEqual(input, result);
        }

        [Test]
        public void FixHtml_OpenAngleBracket_ReplacesWithOpenParen()
        {
            string input = "Hello < World";
            string result = Utility.FixHtml(input);
            Assert.AreEqual("Hello ( World", result);
        }

        [Test]
        public void FixHtml_CloseAngleBracket_ReplacesWithCloseParen()
        {
            string input = "Hello > World";
            string result = Utility.FixHtml(input);
            Assert.AreEqual("Hello ) World", result);
        }

        [Test]
        public void FixHtml_PoundSign_ReplacesWithHyphen()
        {
            string input = "Hello # World";
            string result = Utility.FixHtml(input);
            Assert.AreEqual("Hello - World", result);
        }

        [Test]
        public void FixHtml_MixedSpecialCharacters_ReplacesAll()
        {
            string input = "<Test#1>";
            string result = Utility.FixHtml(input);
            Assert.AreEqual("(Test-1)", result);
        }

        [Test]
        public void FixHtml_MultipleInstances_ReplacesAll()
        {
            string input = "<#><#>";
            string result = Utility.FixHtml(input);
            Assert.AreEqual("(-)(-)", result);
        }
    }
}
