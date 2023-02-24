namespace MarsRobot.App
{
    public enum CommandType
    {
        MoveForward,
        TurnLeft,
        TurnRight
    }

    public interface ICommand
    {

    }

    public class Command
    {
        public CommandType Type { get; set; }
    }

    public class Plateau
    {
        public int Width { get; }
        public int Height { get; }

        public Plateau(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool IsValidPosition(Position position)
        {
            return position.X >= 0 && position.X <= Width && position.Y >= 0 && position.Y <= Height;
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }

    public enum CardinalDirection
    {
        North,
        East,
        South,
        West
    }

    public class Robot
    {
        private Position _position;
        private CardinalDirection _direction;
        private Plateau _plateau;

        public Robot(Position position, CardinalDirection direction, Plateau plateau)
        {
            _position = position;
            _direction = direction;
            _plateau = plateau;
        }

        public void ExecuteCommand(Command command, Plateau plateau)
        {
            switch (command.Type)
            {
                case CommandType.MoveForward:
                    if (_plateau.IsValidPosition(_position))
                    {
                        Move();
                    }
                    break;
                case CommandType.TurnLeft:
                    TurnLeft();
                    break;
                case CommandType.TurnRight:
                    TurnRight();
                    break;
                default:
                    throw new ArgumentException("Invalid command type");
            }
        }

        public void TurnLeft()
        {
            _direction = _direction switch
            {
                CardinalDirection.North => CardinalDirection.West,
                CardinalDirection.East => CardinalDirection.North,
                CardinalDirection.South => CardinalDirection.East,
                CardinalDirection.West => CardinalDirection.South,
                _ => throw new ArgumentException("Invalid direction"),
            };
        }

        public void TurnRight()
        {
            _direction = _direction switch
            {
                CardinalDirection.North => CardinalDirection.East,
                CardinalDirection.East => CardinalDirection.South,
                CardinalDirection.South => CardinalDirection.West,
                CardinalDirection.West => CardinalDirection.North,
                _ => throw new ArgumentException("Invalid direction"),
            };
        }

        public void Move()
        {
            switch (_direction)
            {
                case CardinalDirection.North:
                    if (_position.Y < _plateau.Height)
                    {
                        _position.Y++;
                    }
                    break;
                case CardinalDirection.East:
                    if (_position.X < _plateau.Width)
                    {
                        _position.X++;
                    }
                    break;
                case CardinalDirection.South:
                    if (_position.Y > 0)
                    {
                        _position.Y--;
                    }
                    break;
                case CardinalDirection.West:
                    if (_position.X > 0)
                    {
                        _position.X--;
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid direction");
            }
        }

        public override string ToString()
        {
            return $"{_position.ToString()} {_direction.ToString()}";
        }
    }
}