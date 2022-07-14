namespace adventofcode2021
{
    public class Day3_Part1 : IDay
    {
        public decimal Run(string input)
        {
            return PowerConsumption(input);
        }

        public void Test(string input)
        {
            Assert.AreEqual(PowerConsumption(input), 198);
        }

        private int GetGamma(Position[] positions)
        {
            // gamma is most common bits
            var binaryNumber = string.Join("", positions.Select(p => p.MostCommon()));
            return Convert.ToInt32(binaryNumber, 2);
        }
        private int GetEpsilon(Position[] positions)
        {
            // epsilon is least common bits.
            var binaryNumber = string.Join("", positions.Select(p => p.LeastCommon()));
            return Convert.ToInt32(binaryNumber, 2);
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


        private int PowerConsumption(string input)
        {
            var lines = input.Split('\n');

            var positions = GetPositions(lines);
            var gamma = GetGamma(positions);
            var epsilon = GetEpsilon(positions);
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
            public string MostCommon()
            {
                if (zeroCount > oneCount)
                {
                    return "0";
                }
                return "1";
            }
            public string LeastCommon()
            {
                if (zeroCount < oneCount)
                {
                    return "0";
                }
                return "1";
            }
        }
    }
}