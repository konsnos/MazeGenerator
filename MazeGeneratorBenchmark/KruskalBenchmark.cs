using BenchmarkDotNet.Attributes;
using MazeGenerator;
using MazeGenerator.GenerationAlgorithms;
using MazeGenerator.Graphs;

namespace MazeGeneratorBenchmark;

public class KruskalOverall
{
    [Params(50, 100)]
    public int Size { get; set; }
    
    [Benchmark]
    public void Kruskal()
    {
        _ = Mazes.GetKruskal(Size, Size);
    }
}

public class KruskalModules
{
    [Params(50)]
    public int Size { get; set; }

    private Graph _graph;
    private List<Edge> _edges;

    [Benchmark]
    public void GetKruskalGraph()
    {
        _ = Kruskal.GenerateGraph(Size, Size);
    }

    [GlobalSetup(Target = nameof(GetKruskalSpanningTree))]
    public void GlobalSetupGetKruskalSpanningTree()
    {
        _graph = Kruskal.GenerateGraph(Size, Size);
    }

    [Benchmark]
    public void GetKruskalSpanningTree()
    {
        Kruskal.GetSpanningTree(_graph);
    }
    
    [GlobalSetup(Target = nameof(GetKruskalMap))]
    public void GlobalSetupGetKruskalMap()
    {
        _graph = Kruskal.GenerateGraph(Size, Size);
        _edges = Kruskal.GetSpanningTree(_graph);
    }

    [Benchmark]
    public void GetKruskalMap()
    {
        Mazes.GetMap(Size, Size, _edges);
    }
}