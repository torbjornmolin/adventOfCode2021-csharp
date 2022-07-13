using System.Linq;
namespace adventofcode2021
{
    public class Day5_Part1 : IDay
    {
        public int Run(string input)
        {
            return CountOverlaps(input);
        }

        public void Test(string input)
        {
            // Assert.AreEqual("0,9 -> 5,9", new Line("0,9 -> 5,9").ToString(), "Line ToString roundrips ok");
            // Assert.AreEqual(new Line("0,9 -> 5,9").IsRightAngle, true, "Vertical line 'IsRightAngle");
            // Assert.AreEqual(new Line("0,3 -> 0,9").IsRightAngle, true, "Horizontal line 'IsRightAngle");
            // Assert.AreEqual(new Line("0,3 -> 1,9").IsRightAngle, false, "Non horizontal, non vertical line is not right angle");

            var line1 = new Line("0,0 -> 9,0");
            var line2 = new Line("0,0 -> 9,0");
            var canvas = new Canvas(10, 2);
            canvas.DrawLine(line1);
            canvas.DrawLine(line2);
            Assert.AreEqual(canvas.NumerOfQualifyingOverlaps(), 10, "Qualifying overlaps is ok for horizontal");

            var line3 = new Line("0,0 -> 0,9");
            var line4 = new Line("0,0 -> 0,9");
            var canvas2 = new Canvas(2, 10);
            canvas2.DrawLine(line3);
            canvas2.DrawLine(line4);
            Assert.AreEqual(canvas.NumerOfQualifyingOverlaps(), 10, "Qualifying overlaps is ok for vertical");

            Assert.AreEqual(CountOverlaps(input), 5, "Count overlaps");
        }

        private int CountOverlaps(string input)
        {
            var lines = GetLines(input).ToList();
            var width = lines.Max(l => l.MaxX()) + 1;
            var height = lines.Max(l => l.MaxY()) + 1;

            var canvas = new Canvas(width, height);
            foreach (var line in lines)
            {
                canvas.DrawLine(line);
            }
            return canvas.NumerOfQualifyingOverlaps();
        }

        private IEnumerable<Line> GetLines(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var line in lines)
            {
                yield return new Line(line);
            }
        }

        public class Point
        {
            public int X { get; }
            public int Y { get; }

            public Point(string coordinates)
            {
                var parts = coordinates.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);
                X = int.Parse(parts[0].Trim());
                Y = int.Parse(parts[1].Trim());
            }
            public override string ToString()
            {
                return $"{X},{Y}";
            }
        }
        public class Line
        {
            public Point Point1 { get; private set; }
            public Point Point2 { get; private set; }
            public Line(string input)
            {
                var parts = input.Split("->", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                Point1 = new Point(parts[0]);
                Point2 = new Point(parts[1]);
            }
            public override string ToString()
            {
                return $"{Point1} -> {Point2}";
            }
            public bool IsRightAngle
            {
                get
                {
                    return Point1.X == Point2.X || Point1.Y == Point2.Y;
                }
            }
            public int MaxX() => new int[] { Point1.X, Point2.X }.Max();
            public int MaxY() => new int[] { Point1.Y, Point2.Y }.Max();
        }
        public class Canvas
        {
            private uint[,] canvas;

            public Canvas(int width, int height)
            {
                canvas = new uint[width, height];
            }

            public int NumerOfQualifyingOverlaps()
            {
                int result = 0;
                foreach (var p in canvas)
                {
                    if (p > 1)
                        result++;
                }
                return result;
            }
            public void Print()
            {
                for (int y = 0; y <= canvas.GetUpperBound(1); y++)
                {
                    for (int x = 0; x <= canvas.GetUpperBound(0); x++)
                    {
                        Console.Write(canvas[x, y]);
                    }
                    Console.Write(Environment.NewLine);
                }
            }
            public void DrawLine(Line line)
            {
                if (line.Point1.X == line.Point2.X)
                {
                    var x = line.Point1.X;
                    var minY = Math.Min(line.Point1.Y, line.Point2.Y);
                    var maxY = Math.Max(line.Point1.Y, line.Point2.Y);

                    for (int y = minY; y <= maxY; y++)
                    {
                        canvas[x, y]++;
                    }
                }
                else if (line.Point1.Y == line.Point2.Y)
                {
                    var y = line.Point1.Y;
                    var minX = Math.Min(line.Point1.X, line.Point2.X);
                    var maxX = Math.Max(line.Point1.X, line.Point2.X);

                    for (int x = minX; x <= maxX; x++)
                    {
                        canvas[x, y]++;
                    }
                }
            }
        }
    }
}