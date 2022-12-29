using BenchmarkDotNet.Attributes;
using MazeGenerator;

namespace MazeGeneratorBenchmark;

[MemoryDiagnoser]
public class HuntAndKillOverall
{
    [Params(100, 200)]
    public int Size { get; set; }

    [Benchmark]
    public void HuntAndKill()
    {
        _ = Mazes.GetHuntAndKill(Size, Size);
    }
}