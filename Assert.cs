namespace adventofcode2021
{
    public static class Assert
    {
        public static void AreEqual(int expected, int actual, string info = "")
        {
            if (expected == actual)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.Write($"Test {info}: ");
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"Pass: {expected} == {actual}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine($"Test: {info}");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Fail: {expected} != {actual}");
                Console.ResetColor();
            }
        }

        internal static void IsGreater(int expected, int actual, string info = "")
        {
            if (actual > expected)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.Write($"Test {info}: ");
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"Pass: {expected} == {actual}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine($"Test: {info}");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Fail: {expected} != {actual}");
                Console.ResetColor();
            }
        }

        public static void AreEqual(decimal expected, decimal actual, string info = "")
        {
            if (expected == actual)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.Write($"Test {info}: ");
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"Pass: {expected} == {actual}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine($"Test: {info}");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Fail: {expected} != {actual}");
                Console.ResetColor();
            }
        }




        public static void AreEqual(string expected, string actual, string info = "")
        {
            if (expected == actual)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.Write($"Test {info}: ");
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"Pass: {expected} == {actual}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.Write($"Test {info}: ");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Fail: {expected} != {actual}");
                Console.ResetColor();
            }
        }

        public static void AreEqual(bool expected, bool actual, string info = "")
        {
            if (expected == actual)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.Write($"Test {info}: ");
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"Pass: {expected} == {actual}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.Write($"Test {info}: ");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Fail: {expected} != {actual}");
                Console.ResetColor();
            }
        }
    }
}