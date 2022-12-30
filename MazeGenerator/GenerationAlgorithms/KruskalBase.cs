using System.Collections.Generic;
using MazeGenerator.Graphs;

namespace MazeGenerator.GenerationAlgorithms
{
    public abstract class KruskalBase<GraphType, EdgeType> : IMap
        where GraphType : Graph
        where EdgeType : Edge
    {
        protected readonly int _width;
        protected readonly int _height;

        protected GraphType _graph;
        protected List<EdgeType> _spanningTree;

        protected KruskalBase(int gridWidth, int gridHeight)
        {
            _width = gridWidth;
            _height = gridHeight;
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
            var verticesCoordinates = new GridCoordinates[_width * _height];
            foreach (var edge in _spanningTree)
            {
                verticesCoordinates[edge.Endpoint1] =
                    new GridCoordinates(edge.Endpoint1 % _width, edge.Endpoint1 / _height);
                verticesCoordinates[edge.Endpoint2] =
                    new GridCoordinates(edge.Endpoint2 % _width, edge.Endpoint2 / _height);
            }

            return verticesCoordinates;
        }

        private bool[,] GetMapFromVerticesCoordinates(GridCoordinates[] verticesCoordinates)
        {
            var mapWidth = (_width * 2) + 1;
            var mapHeight = (_height * 2) + 1;

            var map = new bool[mapWidth, mapHeight];
            foreach (var edge in _spanningTree)
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