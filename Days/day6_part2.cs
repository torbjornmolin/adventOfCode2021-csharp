namespace adventofcode2021
{
    public class Day6_Part2 : IDay
    {
        public decimal Run(string input)
        {
            return Simulate(256, Parse(input));
        }

        public void Test(string input)
        {
            Assert.AreEqual(Simulate(256, Parse(input)), 26984457539, "Correct simulation for test data.");
        }

        private decimal Simulate(int days, List<int> fish)
        {
            var fishByAge = new decimal[9];
            foreach (var f in fish)
            {
                fishByAge[f]++;
            }

            for (int i = 0; i < days; i++)
            {
                var newGen = fishByAge[0];
                fishByAge[0] = fishByAge[1];
                fishByAge[1] = fishByAge[2];
                fishByAge[2] = fishByAge[3];
                fishByAge[3] = fishByAge[4];
                fishByAge[4] = fishByAge[5];
                fishByAge[5] = fishByAge[6];
                fishByAge[6] = fishByAge[7] + newGen;
                fishByAge[7] = fishByAge[8];

                fishByAge[8] = newGen;
            }

            return fishByAge.Sum();
        }

        private List<int> Parse(string input)
        {
            return input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList();
        }
    }
}