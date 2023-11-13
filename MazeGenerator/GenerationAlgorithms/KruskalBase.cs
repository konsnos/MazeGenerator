using System.Collections.Generic;
using MazeGenerator.Graphs;
using MazeGenerator.Utils;

namespace MazeGenerator.GenerationAlgorithms
{
    public abstract class KruskalBase<GraphType, EdgeType> : IMap
        where GraphType : Graph
        where EdgeType : Edge
    {
        protected readonly int Width;
        protected readonly int Height;

        protected GraphType Graph;
        protected List<EdgeType> SpanningTree;

        protected KruskalBase(int gridWidth, int gridHeight)
        {
            Width = gridWidth;
            Height = gridHeight;
        }

        protected void Initialise()
        {
            GenerateGraph();
            GetSpanningTree();
        }

        protected abstract void GenerateGraph();

        protected abstract void GetSpanningTree();

        public bool[,] GetMap()
        {
            var verticesCoordinates = GetVerticesCoordinates();

            var map = GetMapFromVerticesCoordinates(verticesCoordinates);

            return map;
        }

        private GridCoordinates[] GetVerticesCoordinates()
        {
            var verticesCoordinates = new GridCoordinates[Width * Height];
            foreach (var edge in SpanningTree)
            {
                verticesCoordinates[edge.Endpoint1] =
                    new GridCoordinates(edge.Endpoint1 % Width, edge.Endpoint1 / Height);
                verticesCoordinates[edge.Endpoint2] =
                    new GridCoordinates(edge.Endpoint2 % Width, edge.Endpoint2 / Height);
            }

            return verticesCoordinates;
        }

        private bool[,] GetMapFromVerticesCoordinates(GridCoordinates[] verticesCoordinates)
        {
            var mapWidth = (Width * 2) + 1;
            var mapHeight = (Height * 2) + 1;

            var map = new bool[mapWidth, mapHeight];
            foreach (var edge in SpanningTree)
            {
                var mapEdge = new MapEdge(verticesCoordinates[edge.Endpoint1], verticesCoordinates[edge.Endpoint2]);

                foreach (var edgeCoordinate in mapEdge.GetAllEdgeCoordinates())
                {
                    map[edgeCoordinate.X, edgeCoordinate.Y] = true;
                }
            }

            return map;
        }
    }
}