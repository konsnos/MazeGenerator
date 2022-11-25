// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using MazeGeneratorBenchmark;

var summary = BenchmarkRunner.Run(typeof(MazeBenchmark).Assembly);