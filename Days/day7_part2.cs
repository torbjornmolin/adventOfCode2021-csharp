namespace adventofcode2021
{
    public class Day7_Part2 : IDay
    {
        public decimal Run(string input)
        {
            return FindCheapestAlignmentPoint(Parse(input));
        }

        public void Test(string input)
        {
            Assert.AreEqual(GetFuelCost(3), 6);
            Assert.AreEqual(FindCheapestAlignmentPoint(Parse(input)), 168, "Correct minimum fuel consumption found");
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
                        return GetFuelCost(v - possiblePosition);
                    return GetFuelCost(possiblePosition - v);
                });
                if (fuelConsumption < minimumFuel)
                    minimumFuel = fuelConsumption;
            }
            return minimumFuel;
        }

        private int GetFuelCost(int steps)
        {
            // S = n(a + l)/2
            // a = first term = 1
            // l = last term = steps
            // n = numer of integers = steps
            // S = steps * ( 1 + steps) / 2

            return (steps * (1 + steps)) / 2;
        }
        private IEnumerable<int> Parse(string input)
        {
            return input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s));
        }
    }
}