namespace adventofcode2021
{
    public class Day2_Part2 : IDay
    {
        public int Run(string input)
        {
            return GetResult(input);
        }

        public void Test(string input)
        {
            Assert.AreEqual(GetResult(input), 900);
        }

        private int GetResult(string input)
        {
            var boat = new Boat();
            foreach (var command in Parse(input))
            {
                boat.Do(command);
            }
            return boat.DepthTimesHorizontal();
        }
        private IEnumerable<Command> Parse(string input)
        {
            var lines = input.Split('\n');
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                var amount = int.Parse(parts[1]);
                var direction = Enum.Parse<Direction>(parts[0]);

                yield return new Command
                {
                    amount = amount,
                    direction = direction
                };
            }

        }

        public enum Direction
        {
            forward,
            down,
            up
        }

        public class Boat
        {
            private int horizontal = 0;
            private int depth = 0;
            private int aim = 0;

            public Boat()
            {
            }
            public int DepthTimesHorizontal()
            {
                return horizontal * depth;
            }
            public void Do(Command command)
            {
                switch (command.direction)
                {
                    case Direction.down:
                        aim += command.amount;
                        break;
                    case Direction.up:
                        aim -= command.amount;
                        break;
                    case Direction.forward:
                        horizontal += command.amount;
                        depth += aim * command.amount;
                        break;
                }
            }
        }
        public struct Command
        {
            public Direction direction;
            public int amount;
        }
    }
}