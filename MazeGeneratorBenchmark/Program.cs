// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using MazeGeneratorBenchmark;

// var benchmarkArgs = new string[] {"-f", "MazeGeneratorBenchmark.KruskalModules.GetKruskalMap"};
// var benchmarkArgs = new string[] {"-f", "MazeGeneratorBenchmark.KruskalOverall.Kruskal"};
var summary = BenchmarkSwitcher.FromAssembly(typeof(KruskalOverall).Assembly).Run(args);