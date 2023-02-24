using MarsRobot.App;
using static MarsRobot.App.Robot;

namespace MarsRobot.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Robot_ShouldFaceNorth_WhenInitialized()
        {
            var robot = new Robot(5, 5);

            Assert.AreEqual(robot.CurrentDirection, Direction.North);
        }
    }
}
}