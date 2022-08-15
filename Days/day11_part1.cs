namespace adventofcode2021
{
    public class Day11_Part1 : IDay
    {
        public decimal Run(string input)
        {
            var grid = new OctopusGrid(input);
            for (int i = 0; i < 100; i++)
            {
                grid.Step();
            }
            return grid.FlashCount();
        }

        public void Test(string input)
        {
            var grid = new OctopusGrid(input);
            for (int i = 0; i < 100; i++)
            {
                grid.Step();
            }
            Assert.AreEqual(grid.FlashCount(), 1656);
        }

        public class OctopusGrid
        {
            public OctopusGrid(string input)
            {
                var tempMap = new List<Octopus[]>();
                foreach (
                    var line in input.Split(
                        Environment.NewLine,
                        StringSplitOptions.RemoveEmptyEntries
                    )
                )
                {
                    tempMap.Add(
                        line.ToCharArray()
                            .Select(c => new Octopus(int.Parse(c.ToString())))
                            .ToArray()
                    );
                }

                map = tempMap.ToArray();
            }

            public void Step()
            {
                // First, the energy level of each octopus increases by 1.

                foreach (var octopusLine in map)
                {
                    foreach (var octopus in octopusLine)
                    {
                        octopus.IncreaseEnergy();
                    }
                }
                // Then, any octopus with an energy level greater than 9 flashes.
                // This increases the energy level of all adjacent octopuses by 1,
                //  including octopuses that are diagonally adjacent.
                // If this causes an octopus to have an energy level greater than 9, it also flashes.
                // This process continues as long as new octopuses keep having their energy level increased beyond 9. (An octopus can only flash at most once per step.)

                while (true)
                {
                    int flashCount = 0;
                    for (int line = 0; line < map.Length; line++)
                    {
                        Octopus[]? octopusLine = map[line];
                        for (int lineIndex = 0; lineIndex < octopusLine.Length; lineIndex++)
                        {
                            Octopus? octopus = octopusLine[lineIndex];
                            // flash if we should flash
                            bool flashed = octopus.CheckAndFlash();
                            // if we flashed, change energy level of adjacant and increase flashCount
                            if (flashed)
                            {
                                flashCount++;
                                OctopusAt(line - 1, lineIndex - 1)?.IncreaseEnergy();
                                OctopusAt(line - 1, lineIndex)?.IncreaseEnergy();
                                OctopusAt(line - 1, lineIndex + 1)?.IncreaseEnergy();

                                OctopusAt(line, lineIndex - 1)?.IncreaseEnergy();
                                OctopusAt(line, lineIndex + 1)?.IncreaseEnergy();

                                OctopusAt(line + 1, lineIndex - 1)?.IncreaseEnergy();
                                OctopusAt(line + 1, lineIndex)?.IncreaseEnergy();
                                OctopusAt(line + 1, lineIndex + 1)?.IncreaseEnergy();
                            }
                        }
                    }
                    if (flashCount == 0)
                        break;
                }

                // Finally, any octopus that flashed during this step has its energy level set to 0, as it used all of its energy to flash.
                foreach (var octopusLine in map)
                {
                    foreach (var octopus in octopusLine)
                    {
                        octopus.Reset();
                    }
                }
            }

            public int FlashCount()
            {
                int result = 0;

                foreach (var line in map)
                {
                    foreach (var octopus in line)
                    {
                        result += octopus.FlashCount;
                    }
                }
                return result;
            }

            private Octopus? OctopusAt(int line, int index)
            {
                if (line < 0)
                    return null;
                if (line > map.Length - 1)
                    return null;
                if (index < 0)
                    return null;
                if (index > map[line].Length - 1)
                    return null;

                return map[line][index];
            }

            private Octopus[][] map;
        }

        public class Octopus
        {
            public Octopus(int energyLevel)
            {
                EnergyLevel = energyLevel;
                State = OctopusState.Default;
                FlashCount = 0;
            }

            public OctopusState State { get; private set; }
            public int EnergyLevel { get; private set; }
            public int FlashCount { get; private set; }

            internal bool CheckAndFlash()
            {
                if (State == OctopusState.HasFlashed)
                {
                    return false;
                }
                if (EnergyLevel < 10)
                {
                    return false;
                }

                State = OctopusState.HasFlashed;
                FlashCount++;

                return true;
            }

            internal void IncreaseEnergy()
            {
                EnergyLevel++;
            }

            internal void Reset()
            {
                if (State == OctopusState.HasFlashed)
                {
                    State = OctopusState.Default;
                    EnergyLevel = 0;
                }
            }

            public enum OctopusState
            {
                Default,
                HasFlashed
            }
        }
    }
}
