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

    public struct GridCoordinates
    {
        public int X { get; }
        public int Y { get; }

        public GridCoordinates(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
    }

    public readonly struct MapEdge
    {
        private GridCoordinates MapCoordinatesFrom { get; }
        private GridCoordinates MapCoordinatesTo { get; }
        private EdgeDirection EdgeDirection { get; }

        public MapEdge(GridCoordinates vertexCoordinatesFrom, GridCoordinates vertexCoordinatesTo)
        {
            MapCoordinatesFrom =
                new GridCoordinates((vertexCoordinatesFrom.X * 2) + 1, (vertexCoordinatesFrom.Y * 2) + 1);
            MapCoordinatesTo = new GridCoordinates((vertexCoordinatesTo.X * 2) + 1, (vertexCoordinatesTo.Y * 2) + 1);

            EdgeDirection = MapCoordinatesFrom.X == MapCoordinatesTo.X ? EdgeDirection.Top : EdgeDirection.Left;
        }

        public GridCoordinates[] GetAllEdgeCoordinates()
        {
            var allCoordinates = new GridCoordinates[3];
            allCoordinates[0] = MapCoordinatesFrom;
            allCoordinates[1] = EdgeDirection == EdgeDirection.Top
                ? new GridCoordinates(MapCoordinatesFrom.X, MapCoordinatesFrom.Y - 1)
                : new GridCoordinates(MapCoordinatesFrom.X - 1, MapCoordinatesFrom.Y);
            allCoordinates[2] = MapCoordinatesTo;
            return allCoordinates;
        }
    }

    public enum EdgeDirection
    {
        Top,
        Left
    }
}