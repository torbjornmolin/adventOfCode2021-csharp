namespace adventofcode2021
{
    public static class Assert
    {
        public static void AreEqual(int expected, int actual)
        {
            if (expected == actual)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"Pass: {expected} == {actual}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Fail: {expected} != {actual}");
                Console.ResetColor();
            }
        }
    }
}