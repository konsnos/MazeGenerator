using System.Collections.Generic;
using MazeGenerator.Graphs;
using MazeGenerator.Utils;

namespace MazeGenerator.GenerationAlgorithms
{
    public class KruskalRandom : KruskalBase<Graph, Edge>
    {
        public KruskalRandom(int gridWidth, int gridHeight) : base(gridWidth, gridHeight)
        {
            Initialise();
        }
        
        protected override void GenerateGraph()
        {
            _graph = new Graph(_width * _height);

            // add edges
            int[,] vertices = new int[_width, _height];
            int index = 0;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    vertices[x, y] = index;
                    // left
                    if (x > 0)
                        _graph.AddEdge(index, vertices[x - 1, y]);
                    // top
                    if (y > 0)
                        _graph.AddEdge(index, vertices[x, y - 1]);
                    index++;
                }
            }
        }
        
        protected override void GetSpanningTree()
        {
            var edges = _graph.GetEdges();
            edges.Shuffle();

            int totalVertices = _graph.Vertices;
            var cycleDetector = new CycleDetector(totalVertices);
            int edgeCount = 0;

            _spanningTree = new List<Edge>();

            foreach (var edge in edges)
            {
                if (cycleDetector.DetectCycle(edge.Endpoint1, edge.Endpoint2))
                    continue;

                _spanningTree.Add(edge);
                edgeCount++;
                if (edgeCount == totalVertices - 1)
                    break;
            }
        }
    }
}