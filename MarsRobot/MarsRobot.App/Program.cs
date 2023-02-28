using MarsRobot.App;

    // Define the robot emoji for each cardinal direction
    const string RobotNorth = "🤖⬆️";
    const string RobotEast = "🤖➡️";
    const string RobotSouth = "🤖⬇️";
    const string RobotWest = "🤖⬅️";

    // Prompt user for plateau size
    Console.Write("Please enter the width and height of the plateau, separated by a x: ");
    string[] plateauSizeStr = Console.ReadLine().Split('x');
    int plateauWidth = int.Parse(plateauSizeStr[0]);
    int plateauHeight = int.Parse(plateauSizeStr[1]);

    // Create plateau object
    Plateau plateau = new Plateau(plateauWidth, plateauHeight);

    // Prompt user for initial robot position and direction
    Console.Write("Please enter the initial position and direction of the robot, separated by a space 1 2 N: ");
    string[] initialPositionStr = Console.ReadLine().Split(' ');
    int initialX = int.Parse(initialPositionStr[0]);
    int initialY = int.Parse(initialPositionStr[1]);
    CardinalDirection initialDirection = (CardinalDirection)Enum.Parse(typeof(CardinalDirection), initialPositionStr[2]);

    // Create robot object
    Robot robot = new Robot(new Position(initialX, initialY), initialDirection, plateau);

    // Prompt user for commands
    Console.Write("Please enter a series of commands for the robot (F = move forward, L = turn left, R = turn right): ");
    string commandsStr = Console.ReadLine().ToUpper();

    // Execute commands and show robot movement on the console
    foreach (char c in commandsStr)
    {
        Command command = new Command();
        switch (c)
        {
            case 'F':
                command.Type = CommandType.F;
                break;
            case 'L':
                command.Type = CommandType.L;
                break;
            case 'R':
                command.Type = CommandType.R;
                break;
            default:
                Console.WriteLine($"Invalid command '{c}', skipping...");
                continue;
        }
        robot.ExecuteCommand(command, plateau);

        // Show the robot's position and direction on the console using emoji
        string robotEmoji = "";
        switch (robot.Direction)
        {
            case CardinalDirection.N:
                robotEmoji = RobotNorth;
                break;
            case CardinalDirection.E:
                robotEmoji = RobotEast;
                break;
            case CardinalDirection.S:
                robotEmoji = RobotSouth;
                break;
            case CardinalDirection.W:
                robotEmoji = RobotWest;
                break;
        }
        Console.Clear(); // Clear the console to update the robot's position
    }

    // Output final robot position and direction
    Console.WriteLine($"Final position: {robot}");
    Console.WriteLine("Press any key to exit.");
    Console.ReadKey();
    Console.ReadLine();
