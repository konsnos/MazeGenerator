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
            var edges = graph.GetEdges();
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

        public static bool[,] GetMap(int width, int height, List<Edge> spanningTree)
        {
            var mapWidth = (width * 2) + 1;
            var mapHeight = (height * 2) + 1;
            
            var verticesCoordinates = new GridCoordinates[width * height];
            foreach (var edge in spanningTree)
            {
                verticesCoordinates[edge.Endpoint1] =
                    new GridCoordinates(edge.Endpoint1 % width, edge.Endpoint1 / height);
                verticesCoordinates[edge.Endpoint2] =
                    new GridCoordinates(edge.Endpoint2 % width, edge.Endpoint2 / height);
            }
            
            var map = new bool[mapWidth, mapHeight];
            foreach (var edge in spanningTree)
            {
                var mapEdge = new MapEdge(verticesCoordinates[edge.Endpoint1], verticesCoordinates[edge.Endpoint2]);

                foreach (var edgeCoordinate in mapEdge.GetAllEdgeCoordinates())
                {
                    map[edgeCoordinate.X, edgeCoordinate.Y] = true;
                }
            }

            return map;
        }

        private static int CompareEdges(EdgeWeighted x, EdgeWeighted y)
        {
            return x.Weight.CompareTo(y.Weight);
        }
    }
}