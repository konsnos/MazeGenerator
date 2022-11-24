using System;
using System.Collections.Generic;
using System.Text;
using MazeGenerator.GenerationAlgorithms;
using MazeGenerator.Graphs;

namespace MazeGenerator
{
    public class Mazes
    {
        public Mazes()
        {
            Console.WriteLine("MazeGenerator initialised");
        }

        public void GetKruskal()
        {
            int width = 10;
            int height = 10;
            var graph = Kruskal.GenerateGraph(width, height);
            // Console.Write(graph.ToString());
            var spanningTree = Kruskal.GetSpanningTree(graph);

            // PrintEdges(spanningTree);

            PrintMap(width, height, spanningTree);
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

        private static void PrintMap(int width, int height, List<Edge> spanningTree)
        {
            var mapWidth = (width * 2) + 1;
            var mapHeight = (height * 2) + 1;
            
            int[,] vertices = new int[width, height];
            var verticesCoordinates = new GridCoordinates[width * height];
            
            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    vertices[x, y] = index;
                    foreach (var edge in spanningTree)
                    {
                        if (edge.Endpoint1 == index || edge.Endpoint2 == index)
                        {
                            verticesCoordinates[index] = new GridCoordinates(x, y);
                        }
                    }
                    index++;
                }
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

    public struct MapEdge
    {
        public GridCoordinates MapCoordinatesFrom { get; }
        public GridCoordinates MapCoordinatesTo { get; }
        public EdgeDirection EdgeDirection { get; }
        
        public MapEdge(GridCoordinates vertexCoordinatesFrom, GridCoordinates vertexCoordinatesTo)
        {
            MapCoordinatesFrom =
                new GridCoordinates((vertexCoordinatesFrom.X * 2) + 1, (vertexCoordinatesFrom.Y * 2) + 1);
            MapCoordinatesTo = new GridCoordinates((vertexCoordinatesTo.X * 2) + 1, (vertexCoordinatesTo.Y * 2) + 1);

            if (MapCoordinatesFrom.X == MapCoordinatesTo.X)
            {
                EdgeDirection = EdgeDirection.Top;
            }
            else
            {
                EdgeDirection = EdgeDirection.Left;
            }
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