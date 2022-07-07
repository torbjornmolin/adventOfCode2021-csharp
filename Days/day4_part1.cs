namespace adventofcode2021
{
    public class Day4_Part1 : IDay
    {
        public int Run(string input)
        {
            return PlayGame(input);
        }

        public void Test(string input)
        {
            Assert.AreEqual(PlayGame(input), 4512);
        }

        private int PlayGame(string input)
        {
            return 0;
        }


        class BingoBoard
        {
            BingoBoard(string input)
            {
                numbers = input.Split(',').Select(n => int.Parse(n)).ToArray();
            }
            private int[] numbers;
        }
    }
}