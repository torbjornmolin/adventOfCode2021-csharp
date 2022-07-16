namespace adventofcode2021
{
    public class Day7_Part1 : IDay
    {
        public decimal Run(string input)
        {
            return FindCheapestAlignmentPoint(Parse(input));
        }

        public void Test(string input)
        {
            Assert.AreEqual(FindCheapestAlignmentPoint(Parse(input)), 37, "Correct minimum fuel consumption found");
        }

        private int FindCheapestAlignmentPoint(IEnumerable<int> input)
        {
            int minimumFuel = int.MaxValue;
            int minimumPosition = input.Min();
            int maximumPosition = input.Max();

            for (int possiblePosition = minimumPosition; possiblePosition <= maximumPosition; possiblePosition++)
            {
                var fuelConsumption = input.Sum(v =>
                {
                    if (v > possiblePosition)
                        return v - possiblePosition;
                    return possiblePosition - v;
                });
                if (fuelConsumption < minimumFuel)
                    minimumFuel = fuelConsumption;
            }
            return minimumFuel;
        }

        private IEnumerable<int> Parse(string input)
        {
            return input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s));
        }
    }
}