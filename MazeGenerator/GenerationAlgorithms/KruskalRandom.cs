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
            Graph = new Graph(Width * Height);

            // add edges
            int[,] vertices = new int[Width, Height];
            int index = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    vertices[x, y] = index;
                    // left
                    if (x > 0)
                        Graph.AddEdge(index, vertices[x - 1, y]);
                    // top
                    if (y > 0)
                        Graph.AddEdge(index, vertices[x, y - 1]);
                    index++;
                }
            }
        }
        
        protected override void GetSpanningTree()
        {
            var edges = Graph.GetEdges();
            edges.Shuffle();

            int totalVertices = Graph.Vertices;
            var cycleDetector = new CycleDetector(totalVertices);
            int edgeCount = 0;

            SpanningTree = new List<Edge>();

            foreach (var edge in edges)
            {
                if (cycleDetector.DetectCycle(edge.Endpoint1, edge.Endpoint2))
                    continue;

                SpanningTree.Add(edge);
                edgeCount++;
                if (edgeCount == totalVertices - 1)
                    break;
            }
        }
    }
}