using BenchmarkDotNet.Attributes;
using MazeGenerator;
using MazeGenerator.GenerationAlgorithms;

namespace MazeGeneratorBenchmark;

public class KruskalOverall
{
    [Params(100, 200)]
    public int Size { get; set; }
    
    [Benchmark]
    public void KruskalRandom()
    {
        _ = Mazes.GetKruskal(Size, Size);
    }
    
    [Benchmark]
    public void KruskalPassingBias()
    {
        _ = Mazes.GetKruskalWithPassingBias(Size, Size, KruskalWeighted.BiasDirection.Horizontal, 1f);
    }
}