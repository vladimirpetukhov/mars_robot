using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace MarsRobot.App
{
    public enum CommandType
    {
        MoveForward,
        TurnLeft,
        TurnRight
    }

    public class Command
    {
        public CommandType Type { get; set; }
    }

    public class Plateau
    {

        private int _width;
        private int _height;

        public Plateau(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public int Width
        {
            get { return _width; }
            private set
            {
                if (!DimensionIsValid(value))
                {
                    throw new ArgumentException();
                }
                _width = value;
            }
        }

        public int Height
        {
            get { return _height; }
            private set
            {
                if (!DimensionIsValid(value))
                {
                    throw new ArgumentException();
                }
                _height = value;
            }
        }

        public bool IsValidPosition(Position position)
        {
            return position.X >= 0 && position.X < Width &&
                   position.Y >= 0 && position.Y < Height;
        }

        private bool DimensionIsValid(int? value)
        {
            if (!value.HasValue || value <= 0)
            {
                return false;
            }

            return true;
        }
    }

    public class Position : IEquatable<Position>
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

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Position other = (Position)obj;

            return X == other.X && Y == other.Y;
        }

        public bool Equals(Position? other)
        {
            if (other == null)
            {
                return false;
            }

            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
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

        private Plateau _plateau;

        public Robot(Position position, CardinalDirection direction, Plateau plateau)
        {
            Position = position;
            Direction = direction;
            _plateau = plateau ?? throw new ArgumentNullException(nameof(plateau));
        }

        public Position Position { get; private set; }

        public CardinalDirection Direction { get; private set; }

        public void TurnLeft()
        {
            Direction = Direction switch
            {
                CardinalDirection.North => CardinalDirection.West,
                CardinalDirection.West => CardinalDirection.South,
                CardinalDirection.South => CardinalDirection.East,
                CardinalDirection.East => CardinalDirection.North,
                _ => throw new InvalidOperationException("Invalid direction"),
            };
        }

        public void TurnRight()
        {
            Direction = Direction switch
            {
                CardinalDirection.North => CardinalDirection.East,
                CardinalDirection.East => CardinalDirection.South,
                CardinalDirection.South => CardinalDirection.West,
                CardinalDirection.West => CardinalDirection.North,
                _ => throw new InvalidOperationException("Invalid direction"),
            };
        }

        public void MoveForward()
        {
            var newPosition = Direction switch
            {
                CardinalDirection.North => new Position(Position.X, Position.Y + 1),
                CardinalDirection.South => new Position(Position.X, Position.Y - 1),
                CardinalDirection.East => new Position(Position.X + 1, Position.Y),
                CardinalDirection.West => new Position(Position.X - 1, Position.Y),
                _ => throw new InvalidOperationException("Invalid direction"),
            };

            if (_plateau != null && _plateau.IsValidPosition(newPosition))
            {
                Position = newPosition;
            }
        }

        public void ExecuteCommand(Command command, Plateau plateau)
        {
            _plateau = plateau;
            switch (command.Type)
            {
                case CommandType.TurnLeft:
                    TurnLeft();
                    break;
                case CommandType.TurnRight:
                    TurnRight();
                    break;
                case CommandType.MoveForward:
                    MoveForward();
                    break;
                default:
                    throw new InvalidOperationException("Invalid command type");
            }
        }

        public override string ToString()
        {
            return $"{Position.X},{Position.Y} {Enum.GetName(Direction)}";
        }
    }
}