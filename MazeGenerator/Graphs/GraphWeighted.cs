using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGenerator.Graphs
{
    public class GraphWeighted : Graph
    {
#if SET_SEED
        private static readonly Random _random = new Random(0);
#else
        private static readonly Random _random = new Random();
#endif

        public GraphWeighted(int vertices) : base(vertices)
        {
            
        }
        
        public void AddEdge(int endpoint1, int endpoint2, float ratio)
        {
            var edge = new EdgeWeighted(endpoint1, endpoint2, (float)_random.NextDouble() * ratio);
            AddEdge(edge);
        }

        public override void AddEdge(int endpoint1, int endpoint2)
        {
            var edge = new EdgeWeighted(endpoint1, endpoint2, (float)_random.NextDouble());
            AddEdge(edge);
        }

        public void AddEdge(EdgeWeighted edgeWeighted)
        {
            int endpoint1 = edgeWeighted.Endpoint1;
            int endpoint2 = edgeWeighted.Endpoint2;
            _adj[endpoint1].AddFirst(edgeWeighted);
            _adj[endpoint2].AddFirst(edgeWeighted);
            _edges++;
        }

        public new List<EdgeWeighted> GetEdges()
        {
            var list = new List<EdgeWeighted>();
            
            for (int vertexIndex = 0; vertexIndex < Vertices; vertexIndex++)
            {
                int selfLoops = 0;

                foreach (var edge in Adj(vertexIndex))
                {
                    if (edge.Target(vertexIndex) > vertexIndex)
                    {
                        list.Add((EdgeWeighted)edge);
                    }
                    else if (edge.Target(vertexIndex) == vertexIndex)
                    {
                        if (selfLoops % 2 == 0) 
                            list.Add((EdgeWeighted)edge);
                        selfLoops++;
                    }
                }
            }

            return list;
        }

        public override string ToString()
        {
            string newline = Environment.NewLine;

            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"Vertices:{Vertices}, Edges: {_edges}{newline}");

            for (int v = 0; v < Vertices; v++)
            {
                stringBuilder.Append($"Vertex {v} edges: ");
                
                foreach (var edge in _adj[v])
                {
                    stringBuilder.Append($"{edge}  ");
                }
                
                stringBuilder.Append(newline);
            }
            return stringBuilder.ToString();
        }
    }
}