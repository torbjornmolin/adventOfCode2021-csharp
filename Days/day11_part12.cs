namespace adventofcode2021
{
    public class Day11_Part2 : IDay
    {
        public decimal Run(string input)
        {
            var grid = new OctopusV2GridV2(input);

            int stepCount = 1;
            while (!grid.Step())
            {
                stepCount++;
            }
            return stepCount;
        }

        public void Test(string input)
        {
            var grid = new OctopusV2GridV2(input);

            int stepCount = 1;
            while (!grid.Step())
            {
                stepCount++;
            }

            Assert.AreEqual(stepCount, 195);
        }

        public class OctopusV2GridV2
        {
            public OctopusV2GridV2(string input)
            {
                var tempMap = new List<OctopusV2[]>();
                foreach (
                    var line in input.Split(
                        Environment.NewLine,
                        StringSplitOptions.RemoveEmptyEntries
                    )
                )
                {
                    tempMap.Add(
                        line.ToCharArray()
                            .Select(c => new OctopusV2(int.Parse(c.ToString())))
                            .ToArray()
                    );
                }

                map = tempMap.ToArray();
            }

            public bool Step()
            {
                // First, the energy level of each OctopusV2 increases by 1.

                foreach (var OctopusV2Line in map)
                {
                    foreach (var OctopusV2 in OctopusV2Line)
                    {
                        OctopusV2.IncreaseEnergy();
                    }
                }
                // Then, any OctopusV2 with an energy level greater than 9 flashes.
                // This increases the energy level of all adjacent OctopusV2es by 1,
                //  including OctopusV2es that are diagonally adjacent.
                // If this causes an OctopusV2 to have an energy level greater than 9, it also flashes.
                // This process continues as long as new OctopusV2es keep having their energy level increased beyond 9. (An OctopusV2 can only flash at most once per step.)

                while (true)
                {
                    int flashCount = 0;
                    for (int line = 0; line < map.Length; line++)
                    {
                        OctopusV2[]? OctopusV2Line = map[line];
                        for (int lineIndex = 0; lineIndex < OctopusV2Line.Length; lineIndex++)
                        {
                            OctopusV2? OctopusV2 = OctopusV2Line[lineIndex];
                            // flash if we should flash
                            bool flashed = OctopusV2.CheckAndFlash();
                            // if we flashed, change energy level of adjacant and increase flashCount
                            if (flashed)
                            {
                                flashCount++;
                                OctopusV2At(line - 1, lineIndex - 1)?.IncreaseEnergy();
                                OctopusV2At(line - 1, lineIndex)?.IncreaseEnergy();
                                OctopusV2At(line - 1, lineIndex + 1)?.IncreaseEnergy();

                                OctopusV2At(line, lineIndex - 1)?.IncreaseEnergy();
                                OctopusV2At(line, lineIndex + 1)?.IncreaseEnergy();

                                OctopusV2At(line + 1, lineIndex - 1)?.IncreaseEnergy();
                                OctopusV2At(line + 1, lineIndex)?.IncreaseEnergy();
                                OctopusV2At(line + 1, lineIndex + 1)?.IncreaseEnergy();
                            }
                        }
                    }
                    if (flashCount == 0)
                        break;
                }

                var result = map.All(
                    line =>
                        line.All(octopus => octopus.State == OctopusV2.OctopusV2State.HasFlashed)
                );

                // Finally, any OctopusV2 that flashed during this step has its energy level set to 0, as it used all of its energy to flash.
                foreach (var OctopusV2Line in map)
                {
                    foreach (var OctopusV2 in OctopusV2Line)
                    {
                        OctopusV2.Reset();
                    }
                }

                return result;
            }

            public int FlashCount()
            {
                int result = 0;

                foreach (var line in map)
                {
                    foreach (var OctopusV2 in line)
                    {
                        result += OctopusV2.FlashCount;
                    }
                }
                return result;
            }

            private OctopusV2? OctopusV2At(int line, int index)
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

            private OctopusV2[][] map;
        }

        public class OctopusV2
        {
            public OctopusV2(int energyLevel)
            {
                EnergyLevel = energyLevel;
                State = OctopusV2State.Default;
                FlashCount = 0;
            }

            public OctopusV2State State { get; private set; }
            public int EnergyLevel { get; private set; }
            public int FlashCount { get; private set; }

            internal bool CheckAndFlash()
            {
                if (State == OctopusV2State.HasFlashed)
                {
                    return false;
                }
                if (EnergyLevel < 10)
                {
                    return false;
                }

                State = OctopusV2State.HasFlashed;
                FlashCount++;

                return true;
            }

            internal void IncreaseEnergy()
            {
                EnergyLevel++;
            }

            internal void Reset()
            {
                if (State == OctopusV2State.HasFlashed)
                {
                    State = OctopusV2State.Default;
                    EnergyLevel = 0;
                }
            }

            public enum OctopusV2State
            {
                Default,
                HasFlashed
            }
        }
    }
}
