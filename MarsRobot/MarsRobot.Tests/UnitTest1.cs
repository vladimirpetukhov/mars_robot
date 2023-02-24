using MarsRobot.App;
using static MarsRobot.App.Robot;

namespace MarsRobot.Tests
{
    public class Tests
    {
        private Plateau _plateau;

        [SetUp]
        public void Setup()
        {
            _plateau = new Plateau(5, 5);
        }

        [Test]
        public void ExecuteCommand_WhenCommandIsTurnLeft_ShouldTurnRobotLeft()
        {
            // Arrange
            var position = new Position(2, 3);
            var direction = CardinalDirection.North;
            var plateau = new Plateau(5, 5);
            var robot = new Robot(position, direction, plateau);
            var command = new Command { Type = CommandType.TurnLeft };

            // Act
            robot.ExecuteCommand(command, plateau);

            // Assert
            Assert.AreEqual(CardinalDirection.West, robot.Direction);
        }

        [Test]
        public void ExecuteCommand_WhenCommandIsTurnRight_ShouldTurnRobotRight()
        {
            // Arrange
            var position = new Position(2, 3);
            var direction = CardinalDirection.North;
            var plateau = new Plateau(5, 5);
            var robot = new Robot(position, direction, plateau);
            var command = new Command { Type = CommandType.TurnRight };

            // Act
            robot.ExecuteCommand(command, plateau);

            // Assert
            Assert.AreEqual(CardinalDirection.East, robot.Direction);
        }

        [Test]
        public void ExecuteCommand_WhenCommandIsMoveForward_AndRobotCanMove_ShouldMoveRobot()
        {
            // Arrange
            var position = new Position(2, 3);
            var direction = CardinalDirection.North;
            var plateau = new Plateau(5, 5);
            var robot = new Robot(position, direction, plateau);
            var command = new Command { Type = CommandType.MoveForward };

            // Act
            robot.ExecuteCommand(command, plateau);

            // Assert
            Assert.AreEqual(new Position(2, 4), robot.Position);
        }

        [Test]
        public void ExecuteCommand_WhenCommandIsMoveForward_AndRobotCannotMoveOutsidePlateau_ShouldNotMoveRobot()
        {
            // Arrange
            var position = new Position(5, 5);
            var direction = CardinalDirection.North;
            var plateau = new Plateau(5, 5);
            var robot = new Robot(position, direction, plateau);
            var command = new Command { Type = CommandType.MoveForward };

            // Act
            robot.ExecuteCommand(command, plateau);

            // Assert
            Assert.AreEqual(new Position(5, 5), robot.Position);
        }

        [Test]
        public void ExecuteCommand_WhenCommandIsMoveForward_AndRobotCannotMove_ShouldNotMoveRobot()
        {
            // Arrange
            var position = new Position(2, 5);
            var direction = CardinalDirection.North;
            var plateau = new Plateau(5, 5);
            var robot = new Robot(position, direction, plateau);
            var command = new Command { Type = CommandType.MoveForward };
            robot.TurnRight();  // turn robot to face the edge of the plateau

            // Act
            robot.ExecuteCommand(command, plateau);

            // Assert
            Assert.AreEqual(new Position(2, 5), robot.Position);
        }



        [Test]
        public void TestRobotMove()
        {
            // Arrange
            var robot = new Robot(new Position(1, 2), CardinalDirection.North, _plateau);
            var command = new Command { Type = CommandType.MoveForward };

            // Act
            robot.ExecuteCommand(command, _plateau);

            // Assert
            var expectedPosition = new Position(1, 3);
            Assert.AreEqual(expectedPosition.ToString() + " North", robot.ToString());
        }

        [Test]
        public void TestRobotTurnLeft()
        {
            // Arrange
            var robot = new Robot(new Position(1, 2), CardinalDirection.North, _plateau);
            var command = new Command { Type = CommandType.TurnLeft };

            // Act
            robot.ExecuteCommand(command, _plateau);

            // Assert
            Assert.AreEqual("1,2 West", robot.ToString());
        }

        [Test]
        public void TestRobotTurnRight()
        {
            // Arrange
            var robot = new Robot(new Position(1, 2), CardinalDirection.North, _plateau);
            var command = new Command { Type = CommandType.TurnRight };

            // Act
            robot.ExecuteCommand(command, _plateau);

            // Assert
            Assert.AreEqual("1,2 East", robot.ToString());
        }

        [Test]
        public void TestRobotMoveOffPlateau()
        {
            // Arrange
            var robot = new Robot(new Position(5, 5), CardinalDirection.North, _plateau);
            var command = new Command { Type = CommandType.MoveForward };

            // Act
            robot.ExecuteCommand(command, _plateau);

            // Assert
            Assert.AreEqual("5,5 North", robot.ToString());
        }

        [Test]
        public void TestRobotIsValidPosition()
        {
            // Arrange
            var position = new Position(3, 4);
            var plateau = new Plateau(5, 5);

            // Act
            var result = plateau.IsValidPosition(position);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TestRobotIsNotValidPosition()
        {
            // Arrange
            var position = new Position(-1, 6);
            var plateau = new Plateau(5, 5);

            // Act
            var result = plateau.IsValidPosition(position);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
