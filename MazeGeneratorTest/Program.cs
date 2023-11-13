using MazeGenerator;
using MazeGenerator.Solvers;

if (!GetMazeSize(out int width, out int height)) return 1;

if (!GetAlgorithm(out int algorithmSelection)) return 1;

bool repeat;
do
{
    PrintMap();
    Console.WriteLine("Repeat (y)?");
    var readKey = Console.ReadKey();
    repeat = readKey.KeyChar.Equals('y');
    Console.WriteLine();
} while (repeat);

return 0;

// Methods
bool GetMazeSize(out int width, out int height)
{
    Console.WriteLine("What size width and height do you want the maze (split with space)?");
    var input = Console.ReadLine();

    var sizes = input.Split(" ");
    if (sizes.Length != 2)
    {
        width = height = 0;
        Console.WriteLine("Wrong amount of input");
        return false;
    }

    bool successfulParse = int.TryParse(sizes[0], out width);
    if (!successfulParse)
    {
        height = 0;
        Console.WriteLine("Wrong width");
        return false;
    }

    successfulParse = int.TryParse(sizes[1], out height);
    if (!successfulParse)
    {
        Console.WriteLine("Wrong height");
        return false;
    }

    return true;
}

bool GetAlgorithm(out int selection)
{
    Console.WriteLine(
        "Select generation algorithm\n0. Kruskal\n1. Kruskal Biased\n2. Recursive Backtracking\n3. Hunt And Kill");
    var algorithmSelectionString = Console.ReadLine();
    bool successfulAlgorithm = int.TryParse(algorithmSelectionString, out selection);

    if (successfulAlgorithm) return true;
    
    Console.WriteLine("Couldn't parse input");
    return false;
}

void PrintMap()
{
    bool[,] map;
    switch (algorithmSelection)
    {
        case 0:
            map = Mazes.GetKruskal(width, height);
            break;
        case 1:
            map = Mazes.GetKruskalWithPassingBias(width, height,
                MazeGenerator.GenerationAlgorithms.KruskalWeighted.BiasDirection.Horizontal, .5f);
            break;
        case 2:
            map = Mazes.GetRecursiveBacktracking(width, height);
            break;
        case 3:
            map = Mazes.GetHuntAndKill(width, height);
            break;
        default:
            map = new bool[0, 0];
            Console.WriteLine("Unknown selection");
            break;
    }

    // Mazes.PrintMap(map);
    
    CalculateEntryAndExit(map);

}

void CalculateEntryAndExit(bool[,] map)
{
    var entryExitSearch = new EntryExitSearch(map);
    var gridCoordinates = entryExitSearch.GetEntryAndExit();
    map[gridCoordinates[0].X, gridCoordinates[0].Y] = true;
    map[gridCoordinates[1].X, gridCoordinates[1].Y] = true;
    
    // Mazes.PrintMap(map, gridCoordinates[0], gridCoordinates[1]);
    
    var breadthFirstSearch = new BreadthFirstSearch(map, gridCoordinates[0], gridCoordinates[1]);
    var pathInQueue = breadthFirstSearch.GetPath();

    if (pathInQueue != null)
    {
        var path = pathInQueue.GetPath();
        Mazes.PrintMap(map, gridCoordinates[0], gridCoordinates[1], path);
    }
    else
    {
        Mazes.PrintMap(map, gridCoordinates[0], gridCoordinates[1]);
    }
}