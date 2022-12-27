using MazeGenerator;
using MazeGenerator.GenerationAlgorithms;

// var map = Mazes.GetKruskal(10, 10);
var map = Mazes.GetKruskalWithPassingBias(10, 10, KruskalWeighted.BiasDirection.Horizontal, .5f);
// var map = Mazes.GetRecursiveBacktracking(10, 10);
Mazes.PrintMap(map);