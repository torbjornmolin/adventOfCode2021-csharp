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
            var testBoard = new int[] { 1, 2, 2, 2, 50, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 100 };
            var hasWonBoard = new int[] { -1, -2, -2, -2, -50, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 100 };
            var columnWonBoard = new int[] { -1, 2, 2, 2, 50,
                                             -5, 2, 2, 2, 2,
                                             -2, 2, 2, 2, 2,
                                             -2, 2, -2, 2, 2,
                                              -2, 2, 2, 2, 100 };
            Assert.AreEqual(new BingoBoard(testBoard).GetNumer(0, 4), 50, "GetNumber first row last column.");
            Assert.AreEqual(new BingoBoard(testBoard).GetNumer(0, 0), 1, "GetNumber first row first column");
            Assert.AreEqual(new BingoBoard(testBoard).GetNumer(4, 4), 100, "GetNumber last row last column");

            Assert.AreEqual((int)(new BingoBoard(hasWonBoard).GetState()), (int)BoardState.HasWon, "Winning board row");
            Assert.AreEqual((int)(new BingoBoard(columnWonBoard).GetState()), (int)BoardState.HasWon, "Winning board col");

            Assert.AreEqual(new BingoBoard(columnWonBoard).SumOfUnmarked(), 184, "Sum of unmarked numbers is correct.");
            Assert.AreEqual(Parse(input).BingoBoards.First().GetNumer(4, 4), 19, "Last number in first test board is 19");

            Assert.AreEqual(PlayGame(input), 4512);
        }

        private BingoGame Parse(string input)
        {
            var lines = input.Split(Environment.NewLine);
            var boards = new List<BingoBoard>();
            var numbers = lines.First().Split(',').Select(s =>
            {
                return int.Parse(s.Trim());
            }
                ).ToArray();
            var currentBoard = new List<int>();
            foreach (var line in lines.Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    boards.Add(new BingoBoard(currentBoard.ToArray()));
                    currentBoard = new List<int>();
                    continue;
                }
                currentBoard.AddRange(line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n =>
                {
                    return int.Parse(n.Trim());
                }));
            }
            return new BingoGame(numbers, boards.ToArray());
        }
        private int PlayGame(string input)
        {
            var game = Parse(input);
            foreach (var n in game.Numbers)
            {
                foreach (var board in game.BingoBoards)
                {
                    board.Play(n);
                    if (board.GetState() == BoardState.HasWon)
                    {
                        return n * board.SumOfUnmarked();
                    }
                }
            }
            return 0;
        }

        class BingoGame
        {

            public BingoGame(int[] numbers, BingoBoard[] bingoBoards)
            {
                this.Numbers = numbers;
                BingoBoards = bingoBoards;
            }


            public BingoBoard[] BingoBoards { get; }
            public int[] Numbers { get; }
        }


        internal enum BoardState
        {
            NewBoard,
            HasWon,
        }
        internal class BingoBoard
        {
            internal BingoBoard(int[] input)
            {
                numbers = Chunk(input, 5).ToArray();
            }
            private int[][] numbers;
            internal int GetNumer(int row, int column)
            {
                return numbers[row][column];
            }
            internal int SumOfUnmarked()
            {
                return numbers.Sum(row => row.Sum(n => n >= 0 ? n : 0));
            }
            internal BoardState GetState()
            {
                // Check rows.
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i].All(n => n < 0))
                    {
                        return BoardState.HasWon;
                    }
                }
                // check cols
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (
                        numbers[0][i] < 0
                    && numbers[1][i] < 0
                    && numbers[2][i] < 0
                    && numbers[3][i] < 0
                    && numbers[4][i] < 0
                    )
                    {
                        return BoardState.HasWon;
                    }

                }
                return BoardState.NewBoard;
            }

            internal void Play(int n)
            {
                for (int row = 0; row < numbers.Length; row++)
                {
                    for (int col = 0; col < numbers[row].Length; col++)
                    {
                        if (numbers[row][col] == n)
                        {
                            if (numbers[row][col] == 0)
                            {
                                numbers[row][col] = int.MinValue;
                            }
                            else
                                numbers[row][col] = -n;
                            return;
                        }
                    }
                }
            }
        }

        private static IEnumerable<int[]> Chunk(IEnumerable<int> source, int chunkSize)
        {
            while (source.Any())
            {
                yield return source.Take(chunkSize).ToArray();
                source = source.Skip(chunkSize);
            }
        }
    }
}