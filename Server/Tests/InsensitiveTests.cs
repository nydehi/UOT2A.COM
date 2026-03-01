using System;
using Server;

namespace Server.Tests
{
    public class InsensitiveTests
    {
        public static void Main()
        {
            Console.WriteLine("InsensitiveTests Starting");
            int passed = 0;
            int failed = 0;

            Action<string, bool, bool> AssertEqual = (name, expected, actual) => {
                if (expected == actual) {
                    passed++;
                    Console.WriteLine($"[PASS] {name}");
                } else {
                    failed++;
                    Console.WriteLine($"[FAIL] {name}: Expected {expected}, got {actual}");
                }
            };

            // Test StartsWith
            AssertEqual("StartsWith_Basic_True", true, Insensitive.StartsWith("Hello World", "Hello"));
            AssertEqual("StartsWith_Basic_False", false, Insensitive.StartsWith("Hello World", "World"));
            AssertEqual("StartsWith_CaseInsensitive_True", true, Insensitive.StartsWith("Hello World", "hello"));
            AssertEqual("StartsWith_ExactMatch", true, Insensitive.StartsWith("Hello", "Hello"));
            AssertEqual("StartsWith_ShorterA_False", false, Insensitive.StartsWith("Hi", "Hello"));
            AssertEqual("StartsWith_NullA_False", false, Insensitive.StartsWith(null, "Hello"));
            AssertEqual("StartsWith_NullB_False", false, Insensitive.StartsWith("Hello", null));
            AssertEqual("StartsWith_BothNull_False", false, Insensitive.StartsWith(null, null));
            AssertEqual("StartsWith_EmptyB_True", true, Insensitive.StartsWith("Hello", ""));

            Console.WriteLine($"\nResults: {passed} passed, {failed} failed");
            Environment.Exit(failed == 0 ? 0 : 1);
        }
    }
}
