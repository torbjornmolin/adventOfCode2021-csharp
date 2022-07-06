namespace adventofcode2021
{
    public class Day1_Part1 : IDay
    {
        public int Run(string input)
        {
            return CountIncrements(input);
        }

        public void Test(string input)
        {
            Assert.AreEqual(CountIncrements(input), 7);
        }

        private int CountIncrements(string input)
        {
            var result = 0;
            var parts = input.Split('\n');
            var previous = int.Parse(parts.First());
            foreach (var number in parts.Skip(1))
            {
                var current = int.Parse(number);
                if (current > previous)
                    result++;
                previous = current;
            }

            return result;
        }
    }
}