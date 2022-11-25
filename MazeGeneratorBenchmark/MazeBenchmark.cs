using BenchmarkDotNet.Attributes;
using MazeGenerator;

namespace MazeGeneratorBenchmark;

public class MazeBenchmark
{
    [Params(10, 50, 100)]
    public int Size { get; set; }
    
    [Benchmark]
    public void Kruskal()
    {
        _ = Mazes.GetKruskal(Size, Size);
    }
}