namespace adventofcode2021
{
    public class Day8_Part2 : IDay
    {
        public decimal Run(string input)
        {
            var lines = GetLines(input);

            return lines.Sum(l => l.GetOutputSum());
        }

        public void Test(string input)
        {
            var lines = GetLines(input);
            var digitsExampleLine = new Line("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf").GetDigts();

            Assert.AreEqual(digitsExampleLine.Where(d => d.digit == 0).First().representation, "cagedb".SortedChars(), "Zero is found correctly");
            Assert.AreEqual(digitsExampleLine.Where(d => d.digit == 1).First().representation, "ab".SortedChars(), "One is found correctly");
            Assert.AreEqual(digitsExampleLine.Where(d => d.digit == 2).First().representation, "gcdfa".SortedChars(), "Two is found correctly");
            Assert.AreEqual(digitsExampleLine.Where(d => d.digit == 3).First().representation, "fbcad".SortedChars(), "Three is found correctly");
            Assert.AreEqual(digitsExampleLine.Where(d => d.digit == 4).First().representation, "eafb".SortedChars(), "Four is found correctly");
            Assert.AreEqual(digitsExampleLine.Where(d => d.digit == 5).First().representation, "cdfbe".SortedChars(), "Five is found correctly");
            Assert.AreEqual(digitsExampleLine.Where(d => d.digit == 6).First().representation, "cdfgeb".SortedChars(), "Six is found correctly");
            Assert.AreEqual(digitsExampleLine.Where(d => d.digit == 7).First().representation, "dab".SortedChars(), "Seven is found correctly");
            Assert.AreEqual(digitsExampleLine.Where(d => d.digit == 8).First().representation, "acedgfb".SortedChars(), "Eight is found correctly");
            Assert.AreEqual(digitsExampleLine.Where(d => d.digit == 9).First().representation, "cefabd".SortedChars(), "Nine is found correctly");

            Assert.AreEqual(lines.Sum(l => l.GetOutputSum()), 61229, "Sum of output digits is correct");
        }

        private IEnumerable<Line> GetLines(string input)
        {
            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                yield return new Line(line);
            }
        }
    }

    public class Line
    {
        public Line(string line)
        {
            input = line.Split('|').First().Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(w =>
            {
                return string.Join(string.Empty, w.ToCharArray().OrderBy(c => c));
            }).ToArray();
            output = line.Split('|').Last().Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(w =>
            {
                return string.Join(string.Empty, w.ToCharArray().OrderBy(c => c));
            }).ToArray();
        }
        private string[] input;
        private string[] output;

        public IEnumerable<string> Input { get => input; }
        public IEnumerable<string> Output { get => output; }
        public IEnumerable<string> AllDigits { get => input.Concat(output); }
        private bool ContainsAllElements(string elements, string superset)
        {
            foreach (var c in elements)
            {
                if (superset.Contains(c))
                    continue;
                return false;
            }
            return true;
        }
        public IEnumerable<(int digit, string representation)> GetDigts()
        {
            var one = AllDigits.Where(d => d.Length == 2).First();
            yield return (1, one);

            var four = AllDigits.Where(d => d.Length == 4).First();
            yield return (4, four);

            var seven = AllDigits.Where(d => d.Length == 3).First();
            yield return (7, seven);

            var eight = AllDigits.Where(d => d.Length == 7).First();
            yield return (8, eight);

            // 9 contains all elements of 4 an has 6 segments active.
            var nine = AllDigits.Where(d => d.Length == 6 && ContainsAllElements(four, d)).First();
            yield return (9, nine);

            // 0 contains all elements of 7, but not all of four and has six segments active.

            var zero = AllDigits.Where(d => d.Length == 6
                && ContainsAllElements(seven, d)
                && !ContainsAllElements(four, d)).First();
            yield return (0, zero);

            // 6 contains has 6 segments and is not 9 or 0.
            var six = AllDigits
                .Where(d => d.Length == 6
                && d != nine
                && d != zero).First();
            yield return (6, six);

            // 3 contains all elements of one and has 5 segments active
            var three = AllDigits
                .Where(d => d.Length == 5
                && ContainsAllElements(one, d))
                .First();
            yield return (3, three);

            // 5 has five segemnts active and six contains all elements of five.
            var five = AllDigits
                .Where(d => d.Length == 5
                && ContainsAllElements(d, six))
                .First();
            yield return (5, five);

            // 2 has five segemnts active and is not five or three.
            var two = AllDigits
                .Where(d => d.Length == 5
                && d != five
                && d != three)
                .First();
            yield return (2, two);
        }
        public int GetOutputSum()
        {
            var result = 0;
            var digits = GetDigts();

            int first = digits.Where(d => d.representation == output.ElementAt(0)).First().digit;
            int second = digits.Where(d => d.representation == output.ElementAt(1)).First().digit;
            int third = digits.Where(d => d.representation == output.ElementAt(2)).First().digit;
            int fourth = digits.Where(d => d.representation == output.ElementAt(3)).First().digit;

            result = first * 1000 + second * 100 + third * 10 + fourth;
            return result;
        }
    }

    public static class Extensions
    {
        public static string SortedChars(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return input.ToCharArray().OrderBy(c => c).Stitch();
        }
        public static string Stitch(this IOrderedEnumerable<char> input)
        {
            return input.ToArray().Stitch();
        }
        public static string Stitch(this char[] input)
        {
            if (input is null)
                return string.Empty;
            if (input.Length == 0)
            {
                return string.Empty;
            }
            return string.Join(string.Empty, input);
        }
    }
}