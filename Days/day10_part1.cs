namespace adventofcode2021
{
    public class Day10_Part1 : IDay
    {
        public decimal Run(string input)
        {
            var lines = input.Split(Environment.NewLine).Select(line => new SyntaxLine(line));

            return lines.Sum(l => l.GetScore());
        }

        public void Test(string input)
        {
            var coruptedLine = new SyntaxLine("{([(<{}[<>[]}>{[]{[(<()>");
            Assert.AreEqual((int)coruptedLine.GetState(), (int)SyntaxLine.LineState.Corupted, "Corrupted line");

            var incompleteLine = new SyntaxLine("{[]");
            Assert.AreEqual((int)incompleteLine.GetState(), (int)SyntaxLine.LineState.Incomplete, "Incoplete line");

            var lines = input.Split(Environment.NewLine).Select(line => new SyntaxLine(line));

            Assert.AreEqual(lines.Sum(l => l.GetScore()), 26397, "Correct score for test input");
        }
    }

    public class SyntaxLine
    {
        public enum LineState
        {
            Valid,
            Incomplete,
            Corupted
        }
        private string line;
        public SyntaxLine(string line)
        {
            this.line = line;
        }

        public int GetScore()
        {
            if (GetState() == LineState.Incomplete)
                return 0;

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
                            return GetScore(c);
                        break;
                }
            }
            return 0;
        }

        private int GetScore(char c)
        {
            switch (c)
            {
                case ')':
                    return 3;
                case '}':
                    return 1197;
                case ']':
                    return 57;
                case '>':
                    return 25137;
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