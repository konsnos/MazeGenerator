using System.Collections.Generic;
using MazeGenerator.Graphs;
using MazeGenerator.Utils;

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
             Graph = new GraphWeighted(Width * Height);

            // add edges
            int[,] vertices = new int[Width, Height];
            int index = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    vertices[x, y] = index;
                    // left
                    if (x > 0)
                        Graph.AddEdge(index, vertices[x - 1, y],
                            _biasDirection == BiasDirection.Horizontal ? _biasRatio : 1f);
                    // top
                    if (y > 0)
                        Graph.AddEdge(index, vertices[x, y - 1],
                            _biasDirection == BiasDirection.Vertical ? _biasRatio : 1f);
                    index++;
                }
            }
        }
        
        protected override void GetSpanningTree()
        {
            var edges = Graph.GetEdges();
            edges.Sort();

            int totalVertices = Graph.Vertices;
            var cycleDetector = new CycleDetector(totalVertices);
            int edgeCount = 0;

            SpanningTree = new List<EdgeWeighted>();

            foreach (var edge in edges)
            {
                if (cycleDetector.DetectCycle(edge.Endpoint1, edge.Endpoint2))
                    continue;

                SpanningTree.Add(edge);
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