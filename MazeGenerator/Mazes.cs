using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MazeGenerator.GenerationAlgorithms;
using MazeGenerator.Graphs;

namespace MazeGenerator
{
    public class Mazes
    {
        public static bool[,] GetKruskal(int width, int height)
        {
            var kruskal = new Kruskal(width, height);
            var graph = kruskal.GenerateGraph();
            var spanningTree = kruskal.GetSpanningTree(graph);
            return kruskal.GetMap(spanningTree);
        }

        public static bool[,] GetKruskalWithPassingBias(int width, int height, Kruskal.BiasDirection biasDirection,
            float biasRatio)
        {
            var kruskal = new Kruskal(width, height);
            var graph = kruskal.GenerateGraph(biasDirection, biasRatio);
            var spanningTree = kruskal.GetSpanningTree(graph);
            return kruskal.GetMap(spanningTree.ToList<Edge>());
        }

        public static bool[,] GetRecursiveBacktracking(int width, int height)
        {
            var recursiveBacktracking = new RecursiveBacktracking(width, height);
            return recursiveBacktracking.GetMap();
        }

        private static void PrintEdges(List<Edge> spanningTree)
        {
            spanningTree.Sort(Edge.ComparisonByVertex);
            var stringBuilder = new StringBuilder();
            foreach (var edge in spanningTree)
            {
                stringBuilder.AppendLine(edge.ToString());
            }

            Console.Write(stringBuilder);
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