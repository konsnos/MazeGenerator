using MazeGenerator;
using MazeGenerator.GenerationAlgorithms;

const int width = 10;
const int height = 10;
// var map = Mazes.GetKruskal(width, height);
// var map = Mazes.GetKruskalWithPassingBias(width, height, KruskalWeighted.BiasDirection.Horizontal, .5f);
// var map = Mazes.GetRecursiveBacktracking(width, height);
var map = Mazes.GetHuntAndKill(width, height);
Mazes.PrintMap(map);