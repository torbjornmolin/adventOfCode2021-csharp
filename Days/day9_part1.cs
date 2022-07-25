namespace adventofcode2021
{
    public class Day9_Part1 : IDay
    {
        public decimal Run(string input)
        {
            var map = new HeightMap(input);
            return map.LowPoints().Sum(lp => lp + 1);
        }

        public void Test(string input)
        {
            var map = new HeightMap(input);

            Assert.AreEqual(map.LowPoints().Sum(lp => lp + 1), 15, "Risk level sum is correct");
        }
    }
    public class HeightMap
    {
        private int[][] map; // y, x
        public HeightMap(string input)
        {
            var tempMap = new List<int[]>();

            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                tempMap.Add(line.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray());
            }

            map = tempMap.ToArray();
        }

        public IEnumerable<int> LowPoints()
        {
            for (int y = 0; y < map.Length; y++)
            {
                int[]? line = map[y];
                for (int x = 0; x < line.Length; x++)
                {
                    int point = line[x];
                    if (PointAbove(x, y).HasValue && PointAbove(x, y) <= point)
                        continue;
                    if (PointBelow(x, y).HasValue && PointBelow(x, y) <= point)
                        continue;
                    if (PointToLeft(x, y).HasValue && PointToLeft(x, y) <= point)
                        continue;
                    if (PointToRight(x, y).HasValue && PointToRight(x, y) <= point)
                        continue;
                    yield return point;
                }
            }
        }

        private int? PointAbove(int x, int y)
        {
            return PointAt(x, y - 1);
        }
        private int? PointBelow(int x, int y)
        {
            return PointAt(x, y + 1);
        }
        private int? PointToLeft(int x, int y)
        {
            return PointAt(x - 1, y);
        }
        private int? PointToRight(int x, int y)
        {
            return PointAt(x + 1, y);
        }
        private int? PointAt(int x, int y)
        {
            if (x < 0)
                return null;
            if (x > map[0].Length - 1)
                return null;
            if (y < 0)
                return null;
            if (y > map.Length - 1)
                return null;

            return map[y][x];
        }
    }
}