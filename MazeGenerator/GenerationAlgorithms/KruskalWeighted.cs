using System.Collections.Generic;
using MazeGenerator.Graphs;

namespace MazeGenerator.GenerationAlgorithms
{
    public class KruskalWeighted : KruskalBase<GraphWeighted, EdgeWeighted>
    {
        private readonly BiasDirection _biasDirection;
        private readonly float _biasRatio;
        
        public KruskalWeighted(int gridWidth, int gridHeight, BiasDirection biasDirection, float biasRatio) : base(gridWidth, gridHeight)
        {
            _biasDirection = biasDirection;
            _biasRatio = biasRatio;
            
            Initialise();
        }
        
        protected override void GenerateGraph()
        {
             _graph = new GraphWeighted(_width * _height);

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
                        _graph.AddEdge(index, vertices[x - 1, y],
                            _biasDirection == BiasDirection.Horizontal ? _biasRatio : 1f);
                    // top
                    if (y > 0)
                        _graph.AddEdge(index, vertices[x, y - 1],
                            _biasDirection == BiasDirection.Vertical ? _biasRatio : 1f);
                    index++;
                }
            }
        }
        
        protected override void GetSpanningTree()
        {
            var edges = _graph.GetEdges();
            edges.Sort();

            int totalVertices = _graph.Vertices;
            var cycleDetector = new CycleDetector(totalVertices);
            int edgeCount = 0;

            _spanningTree = new List<EdgeWeighted>();

            foreach (var edge in edges)
            {
                if (cycleDetector.DetectCycle(edge.Endpoint1, edge.Endpoint2))
                    continue;

                _spanningTree.Add(edge);
                edgeCount++;
                if (edgeCount == totalVertices - 1)
                    break;
            }
        }
        
        public enum BiasDirection
        {
            Horizontal,
            Vertical
        }
    }
}