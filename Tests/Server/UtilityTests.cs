using NUnit.Framework;
using Server;

namespace Tests.Server
{
    [TestFixture]
    public class UtilityTests
    {
        [TestCase(0, 0, 0, 0, 0, 0, ExpectedResult = true)] // Same point, 0 range
        [TestCase(1, 1, 0, 0, 0, 2, ExpectedResult = true)] // Inside range
        [TestCase(2, 2, 0, 0, 0, 2, ExpectedResult = true)] // On boundary (Top-Right)
        [TestCase(-2, 2, 0, 0, 0, 2, ExpectedResult = true)] // On boundary (Top-Left)
        [TestCase(2, -2, 0, 0, 0, 2, ExpectedResult = true)] // On boundary (Bottom-Right)
        [TestCase(-2, -2, 0, 0, 0, 2, ExpectedResult = true)] // On boundary (Bottom-Left)
        [TestCase(3, 2, 0, 0, 0, 2, ExpectedResult = false)] // Just outside X boundary
        [TestCase(2, 3, 0, 0, 0, 2, ExpectedResult = false)] // Just outside Y boundary
        [TestCase(0, 0, 10, 0, 0, 0, ExpectedResult = true)] // Z coordinate ignored
        [TestCase(0, 0, 10, 0, 0, -10, ExpectedResult = false)] // Negative range (invalid but handles as false since -10 makes condition fail)
        [TestCase(-10, -10, 0, -5, -5, 5, ExpectedResult = true)] // Negative coords inside range
        [TestCase(-10, -10, 0, -5, -5, 4, ExpectedResult = false)] // Negative coords outside range
        public bool InRange_ShouldReturnCorrectly(int p1x, int p1y, int p1z, int p2x, int p2y, int range)
        {
            Point3D p1 = new Point3D(p1x, p1y, p1z);
            Point3D p2 = new Point3D(p2x, p2y, 0); // Z coordinate of p2 is mostly irrelevant for InRange
            return Utility.InRange(p1, p2, range);
        }
    }
}
