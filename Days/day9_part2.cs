namespace adventofcode2021
{
    public class Day9_Part2 : IDay
    {
        public decimal Run(string input)
        {
            var map = new HeightMapV2(input);
            return map.Answer();
        }

        public void Test(string input)
        {
            var map = new HeightMapV2(input);
            var basinSizes = map.BasinSizes();
            Assert.AreEqual(basinSizes.First(), 3, "First basin is 3");
            Assert.AreEqual(basinSizes.ElementAt(1), 9, "Second basin is 9");

            Assert.AreEqual(basinSizes.Max(), 14, "Max size is 14");
            Assert.AreEqual(map.Answer(), 1134, "Correct answer for test data.");
        }
    }
    public class HeightMapV2
    {
        public class Point
        {
            public int x, y, value;
            public bool PartOfBasin;
        }
        private Point[][] map; // y, x
        public HeightMapV2(string input)
        {
            var tempMap = new List<Point[]>();

            var y = 0;
            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var x = 0;
                tempMap.Add(line.ToCharArray().Select(c => new Point()
                {
                    value = int.Parse(c.ToString()),
                    x = x++,
                    y = y,
                }).ToArray());
                y++;
            }

            map = tempMap.ToArray();
        }

        public int Answer()
        {
            var sizes = BasinSizes().OrderByDescending(b => b).Take(3).ToList();
            return sizes[0] * sizes[1] * sizes[2];
        }
        private IEnumerable<Point> LowPoints()
        {
            for (int y = 0; y < map.Length; y++)
            {
                Point[]? line = map[y];
                for (int x = 0; x < line.Length; x++)
                {
                    int point = line[x].value;
                    if (PointAbove(x, y) is Point pa && pa.value <= point)
                        continue;
                    if (PointBelow(x, y) is Point pb && pb.value <= point)
                        continue;
                    if (PointToLeft(x, y) is Point pl && pl.value <= point)
                        continue;
                    if (PointToRight(x, y) is Point pr && pr.value <= point)
                        continue;
                    yield return new Point
                    {
                        x = x,
                        y = y,
                        value = point
                    };
                }
            }
        }

        public IEnumerable<int> BasinSizes()
        {
            foreach (var p in LowPoints())
            {
                yield return GetBasinSize(p);
            }
        }

        private int GetBasinSize(Point p)
        {
            ResetpartOfBasin();
            PointAt(p.x, p.y)!.PartOfBasin = true;

            // sweep up
            for (int y = p.y; y >= 0; y--)
            {
                TagLine(true, y);
            }
            // sweep down
            for (int y = p.y; y < map.Length; y++)
            {
                TagLine(false, y);
            }

            var result = 0;
            foreach (var line in map)
            {
                foreach (var point in line)
                {
                    if (point.PartOfBasin)
                        result++;
                }
            }

            // foreach (var line in map)
            // {
            //     foreach (var point in line)
            //     {
            //         if (point.PartOfBasin)
            //             Console.ForegroundColor = ConsoleColor.Red;
            //         Console.Write(point.value);
            //         Console.ResetColor();
            //     }
            //     Console.Write(Environment.NewLine);
            // }
            return result;
        }

        public void TagLine(bool scanUp, int y)
        {
            bool inBasin = false;
            // sweep right
            for (int x = 0; x < map[y].Length; x++)
            {
                var point = PointAt(x, y);
                if (point?.PartOfBasin == true)
                {
                    inBasin = true;
                    var horizontalAdjacent = scanUp ? PointAbove(x, y) : PointBelow(x, y);
                    if (horizontalAdjacent is Point && (horizontalAdjacent?.value ?? 9) < 9)
                    {
                        horizontalAdjacent!.PartOfBasin = true;
                    }
                }
                else if (inBasin)
                {
                    if (point!.value == 9)
                    {
                        inBasin = false;
                        continue;
                    }

                    point.PartOfBasin = true;
                    var horizontalAdjacent = scanUp ? PointAbove(x, y) : PointBelow(x, y);
                    if ((horizontalAdjacent?.value ?? 9) < 9)
                    {
                        if (horizontalAdjacent is Point)
                        {
                            horizontalAdjacent!.PartOfBasin = true;
                        }
                    }
                }
            }
            inBasin = false;
            // sweep left
            for (int x = map[y].Length - 1; x >= 0; x--)
            {
                var point = PointAt(x, y);
                if (point!.PartOfBasin == true)
                {
                    inBasin = true;
                }
                else if (inBasin)
                {
                    if (point.value == 9)
                    {
                        inBasin = false;
                        continue;
                    }

                    point.PartOfBasin = true;
                    var horizontalAdjacent = scanUp ? PointAbove(x, y) : PointBelow(x, y);
                    if ((horizontalAdjacent?.value ?? 9) < 9)
                    {
                        if (horizontalAdjacent is Point)
                        {
                            horizontalAdjacent!.PartOfBasin = true;
                        }
                    }
                }
            }
        }

        private void ResetpartOfBasin()
        {
            foreach (var line in map)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    line[i].PartOfBasin = false;
                }
            }
        }

        private Point? PointAbove(int x, int y)
        {
            return PointAt(x, y - 1);
        }
        private Point? PointBelow(int x, int y)
        {
            return PointAt(x, y + 1);
        }
        private Point? PointToLeft(int x, int y)
        {
            return PointAt(x - 1, y);
        }
        private Point? PointToRight(int x, int y)
        {
            return PointAt(x + 1, y);
        }

        public Point? PointAt(int x, int y)
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