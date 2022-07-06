namespace adventofcode2021
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                System.Console.WriteLine("Usage: adventofcode2021-csharp [action] [day]");
                return;
            }
            var day = args.LastOrDefault()?.ToLower() ?? string.Empty;
            var action = args.FirstOrDefault()?.ToLower() ?? string.Empty;

            IDay selectedDay;
            switch (day)
            {
                case "day1_part1":
                    selectedDay = new Day1_Part1();

                    break;
                default:
                    System.Console.WriteLine($"Unknown day: {day}");
                    return;
            }
            switch (action)
            {
                case "test":
                    {
                        string testInput = GetInput(action, day);
                        selectedDay.Test(testInput);
                        break;
                    }
                case "run":
                    {
                        var result = selectedDay.Run(GetInput(action, day));
                        System.Console.WriteLine($"Result was {result}");
                        break;
                    }
                default:
                    System.Console.WriteLine($"Unknown action: {action}");
                    break;

            }
        }

        private static string GetInput(string action, string day)
        {
            // we assume input is in current working directory, named [day].txt or [day]_test.txt
            if (day.Contains("_part1"))
            {
                day = day.Replace("_part1", "");
            }
            if (day.Contains("_part2"))
            {
                day = day.Replace("_part2", "");
            }
            if (action == "test")
                return File.ReadAllText($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{day}_test.txt");
            else
            {
                return File.ReadAllText($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{day}.txt");
            }

        }
    }
}