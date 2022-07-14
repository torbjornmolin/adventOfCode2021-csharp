namespace adventofcode2021
{
    public class Day6_Part1 : IDay
    {
        public decimal Run(string input)
        {
            return Simulate(80, Parse(input));
        }

        public void Test(string input)
        {
            Assert.AreEqual(Simulate(80, Parse(input)), 5934, "Correct simulation for test data.");
        }

        private int Simulate(int days, List<int> fish)
        {
            for (int i = 0; i < days; i++)
            {
                var newFish = new List<int>();
                for (int i1 = 0; i1 < fish.Count; i1++)
                {
                    int f = fish[i1];
                    if (f == 0)
                    {
                        newFish.Add(8);
                        fish[i1] = 6;
                    }
                    else
                    {
                        fish[i1]--;
                    }

                }
                fish = fish.Concat(newFish).ToList();
            }

            return fish.Count();
        }

        private List<int> Parse(string input)
        {
            return input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList();
        }
    }
}