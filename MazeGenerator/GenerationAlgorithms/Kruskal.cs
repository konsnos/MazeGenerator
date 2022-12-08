using System.Collections.Generic;
using MazeGenerator.Graphs;
using MazeGenerator.Utils;

namespace MazeGenerator.GenerationAlgorithms
{
    public class Kruskal
    {
        private int _width;
        private int _height;

        public Kruskal(int gridWidth, int gridHeight)
        {
            _width = gridWidth;
            _height = gridHeight;
        }
        
        public GraphWeighted GenerateGraph(BiasDirection biasDirection, float biasRatio)
        {
            var graph = new GraphWeighted(_width * _height);

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
                        graph.AddEdge(index, vertices[x - 1, y],
                            biasDirection == BiasDirection.Horizontal ? biasRatio : 1f);
                    // top
                    if (y > 0)
                        graph.AddEdge(index, vertices[x, y - 1],
                            biasDirection == BiasDirection.Vertical ? biasRatio : 1f);
                    index++;
                }
            }

            return graph;
        }

        public Graph GenerateGraph()
        {
            var graph = new Graph(_width * _height);

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
                        graph.AddEdge(index, vertices[x - 1, y]);
                    // top
                    if (y > 0)
                        graph.AddEdge(index, vertices[x, y - 1]);
                    index++;
                }
            }

            return graph;
        }

        public List<EdgeWeighted> GetSpanningTree(GraphWeighted graph)
        {
            var edges = graph.GetEdges();
            edges.Sort();

            int totalVertices = graph.Vertices;
            var cycleDetector = new CycleDetector(totalVertices);
            int edgeCount = 0;

            var spanningTree = new List<EdgeWeighted>();

            foreach (var edge in edges)
            {
                if (cycleDetector.DetectCycle(edge.Endpoint1, edge.Endpoint2))
                    continue;

                spanningTree.Add(edge);
                edgeCount++;
                if (edgeCount == totalVertices - 1)
                    break;
            }

            return spanningTree;
        }

        public List<Edge> GetSpanningTree(Graph graph)
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
                edgeCount++;
                if (edgeCount == totalVertices - 1)
                    break;
            }

            return spanningTree;
        }

        public bool[,] GetMap(List<Edge> spanningTree)
        {
            var verticesCoordinates = GetVerticesCoordinates(spanningTree);

            var mapWidth = (_width * 2) + 1;
            var mapHeight = (_height * 2) + 1;
            var map = GetMapFromVerticesCoordinates(spanningTree, mapWidth, mapHeight, verticesCoordinates);

            return map;
        }

        private GridCoordinates[] GetVerticesCoordinates(List<Edge> spanningTree)
        {
            var verticesCoordinates = new GridCoordinates[_width * _height];
            foreach (var edge in spanningTree)
            {
                verticesCoordinates[edge.Endpoint1] =
                    new GridCoordinates(edge.Endpoint1 % _width, edge.Endpoint1 / _height);
                verticesCoordinates[edge.Endpoint2] =
                    new GridCoordinates(edge.Endpoint2 % _width, edge.Endpoint2 / _height);
            }

            return verticesCoordinates;
        }

        private static bool[,] GetMapFromVerticesCoordinates(List<Edge> spanningTree, int mapWidth, int mapHeight,
            GridCoordinates[] verticesCoordinates)
        {
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

        private static int CompareByWeight(EdgeWeighted x, EdgeWeighted y) => x.Weight.CompareTo(y.Weight);
        
        public enum BiasDirection
        {
            Horizontal,
            Vertical
        }
    }
}