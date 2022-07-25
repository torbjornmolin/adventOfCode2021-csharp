namespace adventofcode2021
{
    public class Day10_Part2 : IDay
    {
        public decimal Run(string input)
        {
            var lines = input.Split(Environment.NewLine).Select(line => new SyntaxLineV2(line));
            var incompleteLines = lines.Where(l => l.GetState() == SyntaxLineV2.LineState.Incomplete);

            var scores = incompleteLines.Select(l => l.GetScore());
            return scores.OrderBy(s => s).ElementAt(scores.Count() / 2);
        }

        public void Test(string input)
        {
            var lines = input.Split(Environment.NewLine).Select(line => new SyntaxLineV2(line));
            var incompleteLines = lines.Where(l => l.GetState() == SyntaxLineV2.LineState.Incomplete);

            var scores = incompleteLines.Select(l => l.GetScore()).ToList();

            Assert.AreEqual(scores.First(), 288957, "Correct score for first incomplete line");

            var middleScore = scores.OrderBy(s => s).ElementAt(scores.Count() / 2);

            Assert.AreEqual(middleScore, 288957, "Correct score for middle element.");
        }
    }

    public class SyntaxLineV2
    {
        public enum LineState
        {
            Valid,
            Incomplete,
            Corupted
        }
        private string line;
        public SyntaxLineV2(string line)
        {
            this.line = line;
        }

        public decimal GetScore()
        {
            decimal totalScore = 0;
            if (GetState() != LineState.Incomplete)
                return totalScore;

            var stack = new Stack<char>();
            foreach (var c in line.ToCharArray())
            {
                switch (c)
                {
                    case '(':
                    case '{':
                    case '[':
                    case '<':
                        stack.Push(c);
                        break;
                    case ')':
                    case '}':
                    case ']':
                    case '>':
                        stack.Pop();
                        break;
                }
            }
            while (stack.Count > 0)
            {
                var c = stack.Pop();
                totalScore = totalScore * 5;
                totalScore += GetScore(GetExpected(c));
            }
            return totalScore;
        }

        private int GetScore(char c)
        {
            switch (c)
            {
                case ')':
                    return 1;
                case '}':
                    return 3;
                case ']':
                    return 2;
                case '>':
                    return 4;
                default:
                    throw new InvalidOperationException($"Unexpected character: '{c}'");
            }
        }

        public LineState GetState()
        {
            var stack = new Stack<char>();
            foreach (var c in line.ToCharArray())
            {
                switch (c)
                {
                    case '(':
                    case '{':
                    case '[':
                    case '<':
                        stack.Push(c);
                        break;
                    case ')':
                    case '}':
                    case ']':
                    case '>':
                        var expected = GetExpected(stack.Pop());
                        if (c != expected)
                            return LineState.Corupted;
                        break;
                }
            }
            if (stack.Count != 0)
                return LineState.Incomplete;

            return LineState.Valid;
        }

        private char GetExpected(char c)
        {
            switch (c)
            {
                case '(':
                    return ')';

                case '{':
                    return '}';
                case '[':
                    return ']';
                case '<':
                    return '>';
                default:
                    throw new InvalidOperationException($"Unexpected character: '{c}'");
            }
        }
    }
}