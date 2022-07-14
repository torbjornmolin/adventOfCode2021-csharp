namespace adventofcode2021
{
    public class Day1_Part2 : IDay
    {
        public decimal Run(string input)
        {
            return CountIncrements(GetWindowedSums(GetInputNumbers(input)));
        }

        public void Test(string input)
        {
            Assert.AreEqual(CountIncrements(GetWindowedSums(GetInputNumbers(input))), 5);
        }

        private int[] GetInputNumbers(string input)
        {
            return input.Split('\n').Select(i => int.Parse(i)).ToArray();
        }

        private IEnumerable<int> GetWindowedSums(int[] input)
        {
            for (int i = 0; i < input.Length - 2; i += 1)
            {
                yield return input[i] + input[i + 1] + input[i + 2];
            }
        }
        private int CountIncrements(IEnumerable<int> input)
        {
            var result = 0;
            var previous = input.First();
            foreach (var number in input.Skip(1))
            {
                if (number > previous)
                    result++;
                previous = number;
            }

            return result;
        }
    }
}