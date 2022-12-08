using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using MazeGenerator.GenerationAlgorithms;
using MazeGenerator.Graphs;

namespace MazeGenerator
{
    public class Mazes
    {
        public static bool[,] GetKruskal(int width, int height)
        {
            var graph = Kruskal.GenerateGraph(width, height);
            var spanningTree = Kruskal.GetSpanningTree(graph);
            return Kruskal.GetMap(width, height, spanningTree);
        }

        public static bool[,] GetKruskalWithPassingBias(int width, int height, Kruskal.BiasDirection biasDirection,
            float biasRatio)
        {
            var graph = Kruskal.GenerateGraph(width, height, biasDirection, biasRatio);
            var spanningTree = Kruskal.GetSpanningTree(graph);
            return Kruskal.GetMap(width, height, spanningTree.ToList<Edge>());
        }

        private static void PrintEdges(List<Edge> spanningTree)
        {
            spanningTree.Sort(SortByVertex);
            var stringBuilder = new StringBuilder();
            foreach (var edge in spanningTree)
            {
                stringBuilder.AppendLine(edge.ToString());
            }

            Console.Write(stringBuilder);
        }

        private static int SortByVertex(Edge x, Edge y)
        {
            return x.Endpoint1.CompareTo(y.Endpoint1);
        }

        public static void PrintMap(bool[,] map)
        {
            var mapHeight = map.GetLength(1);
            var mapWidth = map.GetLength(0);

            var stringBuilder = new StringBuilder();

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    stringBuilder.Append(map[x, y] ? "  " : "# ");
                }

                stringBuilder.Append(Environment.NewLine);
            }

            Console.Write(stringBuilder);
        }
    }
}