namespace adventofcode2021
{
    public class Day8_Part1 : IDay
    {
        public decimal Run(string input)
        {
            return Digit_1_4_7_8_count(input);
        }

        public void Test(string input)
        {
            Assert.AreEqual(Digit_1_4_7_8_count(input), 26);
        }

        private int Digit_1_4_7_8_count(string input)
        {
            var oneCount = 0;
            var fourCount = 0;
            var sevenCount = 0;
            var eightCount = 0;
            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var digits = ParseLine(line);
                oneCount += digits.Where(d => d.Length == 2).Count();
                fourCount += digits.Where(d => d.Length == 4).Count();
                sevenCount += digits.Where(d => d.Length == 3).Count();
                eightCount += digits.Where(d => d.Length == 7).Count();
            }

            return oneCount + fourCount + sevenCount + eightCount;
        }

        private IEnumerable<string> ParseLine(string input)
        {
            return input.Split('|').Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}