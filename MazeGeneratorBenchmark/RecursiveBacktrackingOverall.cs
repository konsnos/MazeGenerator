using BenchmarkDotNet.Attributes;
using MazeGenerator;

namespace MazeGeneratorBenchmark;

[MemoryDiagnoser]
public class RecursiveBacktrackingOverall
{
    [Params(100, 200)]
    public int Size { get; set; }

    [Benchmark]
    public void RecursiveBacktracking()
    {
        _ = Mazes.GetRecursiveBacktracking(Size, Size);
    }
}