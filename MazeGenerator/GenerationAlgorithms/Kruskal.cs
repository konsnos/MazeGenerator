using System.Collections.Generic;
using MazeGenerator.Graphs;
using MazeGenerator.Utils;

namespace MazeGenerator.GenerationAlgorithms
{
    public class Kruskal
    {
        public static Graph GenerateGraph(int width, int height)
        {
            var graph = new Graph(width * height);

            // add edges
            int[,] vertices = new int[width, height];
            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    vertices[x, y] = index;
                    // left
                    if (x > 0)
                        graph.AddEdge(index, vertices[x - 1, y]);
                    // top
                    if (y > 0)
                        graph.AddEdge(index, vertices[x, y - 1]);
                    index++;
                }
            }

            return graph;
        }

        public static List<Edge> GetSpanningTree(Graph graph)
        {
            var edges = graph.Edges();
            edges.Shuffle();

            int totalVertices = graph.Vertices;
            var cycleDetector = new CycleDetector(totalVertices);
            int edgeCount = 0;

            var spanningTree = new List<Edge>();

            foreach (var edge in edges)
            {
                if (cycleDetector.DetectCycle(edge.Endpoint1, edge.Endpoint2))
                    continue;

                spanningTree.Add(edge);
                if (edgeCount == totalVertices - 1)
                    break;
            }

            return spanningTree;
        }

        private static int CompareEdges(EdgeWeighted x, EdgeWeighted y)
        {
            return x.Weight.CompareTo(y.Weight);
        }
    }
}