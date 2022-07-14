namespace adventofcode2021
{
    public class Day3_Part2 : IDay
    {
        public decimal Run(string input)
        {
            return LifeSupportRating(input);
        }

        public void Test(string input)
        {
            Assert.AreEqual(GetOxygenRating(GetPositions(input.Split('\n')), input.Split('\n')), 23);
            Assert.AreEqual(GetCO2ScrubberRating(GetPositions(input.Split('\n')), input.Split('\n')), 10);
            Assert.AreEqual(LifeSupportRating(input), 230);
        }

        private int GetOxygenRating(Position[] positions, string[] lines)
        {
            // Oxygen uses most common.
            var binaryNumber = Filter(lines, false);
            return Convert.ToInt32(binaryNumber, 2);
        }
        private int GetCO2ScrubberRating(Position[] positions, string[] lines)
        {
            // CO2 scrubber uses least common bits
            var binaryNumber = Filter(lines, true);
            return Convert.ToInt32(binaryNumber, 2);
        }

        private string Filter(string[] lines, bool leastCommon)
        {
            var currentList = lines.ToList();
            var positions = GetPositions(currentList.ToArray());
            for (int i = 0; i < positions.Length; i++)
            {
                var iterationList = new List<string>();
                foreach (var line in currentList)
                {
                    if (leastCommon)
                    {
                        if (line.ElementAt(i) == positions[i].LeastCommon())
                        {
                            iterationList.Add(line);
                        }
                    }
                    else
                    {
                        if (line.ElementAt(i) == positions[i].MostCommon())
                        {
                            iterationList.Add(line);
                        }
                    }
                }
                if (iterationList.Count == 1)
                    return iterationList.First();
                currentList = iterationList;
                positions = GetPositions(currentList.ToArray());
            }
            throw new Exception("No line found!");
        }
        private Position[] GetPositions(string[] lines)
        {
            var length = lines.First(l => !string.IsNullOrWhiteSpace(l)).Length;

            var positions = new List<Position>();
            for (int i = 0; i < length; i++)
            {
                positions.Add(new Position());
            }

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                for (int i = 0; i < positions.Count; i++)
                {
                    positions[i].Add(line.ElementAt(i));
                }
            }
            return positions.ToArray();
        }


        private int LifeSupportRating(string input)
        {
            var lines = input.Split('\n');

            var positions = GetPositions(lines);
            var gamma = GetOxygenRating(positions, lines);
            var epsilon = GetCO2ScrubberRating(positions, lines);
            return (gamma * epsilon);
        }

        class Position
        {
            private int zeroCount = 0;
            private int oneCount = 0;
            public void Add(char c)
            {
                switch (c)
                {
                    case '0':
                        zeroCount++;
                        break;
                    case '1':
                        oneCount++;
                        break;
                    default:
                        throw new InvalidOperationException($"Unexpected char: '{c}'");
                }
            }
            public char MostCommon()
            {
                if (zeroCount == oneCount)
                {
                    return '1';
                }
                if (zeroCount > oneCount)
                {
                    return '0';
                }
                return '1';
            }
            public char LeastCommon()
            {
                if (zeroCount == oneCount)
                {
                    return '0';
                }
                if (zeroCount < oneCount)
                {
                    return '0';
                }
                return '1';
            }
        }
    }
}