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

    private Kruskal _kruskal;
    private Graph _graph;
    private List<Edge> _edges;

    [Benchmark]
    public void GetKruskalGraph()
    {
        var kruskal = new Kruskal(Size, Size);
        _ = kruskal.GenerateGraph();
    }

    [GlobalSetup(Target = nameof(GetKruskalSpanningTree))]
    public void GlobalSetupGetKruskalSpanningTree()
    {
        _kruskal = new Kruskal(Size, Size);
        _graph = _kruskal.GenerateGraph();
    }

    [Benchmark]
    public void GetKruskalSpanningTree()
    {
        _kruskal.GetSpanningTree(_graph);
    }
    
    [GlobalSetup(Target = nameof(GetKruskalMap))]
    public void GlobalSetupGetKruskalMap()
    {
        _kruskal = new Kruskal(Size, Size);
        _graph = _kruskal.GenerateGraph();
        _edges = _kruskal.GetSpanningTree(_graph);
    }

    [Benchmark]
    public void GetKruskalMap()
    {
        _kruskal.GetMap(_edges);
    }
}