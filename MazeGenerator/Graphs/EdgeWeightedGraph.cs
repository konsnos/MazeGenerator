using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGenerator.Graphs
{
    public class EdgeWeightedGraph
    {
        public int Vertices { get; }
        private int _edges;
        private LinkedList<Edge>[] _adj;

#if SET_SEED
        private static readonly Random _random = new Random(0);
#else
        private static readonly Random _random = new Random();
#endif

        public EdgeWeightedGraph(int vertices)
        {
            if (vertices < 0)
                throw new Exception("Number of vertices in a Graph must be nonnegative");

            Vertices = vertices;
            
            _edges = 0;

            _adj = new LinkedList<Edge>[vertices];

            for (int v = 0; v < vertices; v++)
            {
                _adj[v] = new LinkedList<Edge>();
            }
        }

        public void AddEdge(int endpoint1, int endpoint2)
        {
            var edge = new Edge(endpoint1, endpoint2, (float)_random.NextDouble());
            AddEdge(edge);
        }

        public void AddEdge(Edge edge)
        {
            int endpoint1 = edge.Endpoint1;
            int endpoint2 = edge.Endpoint2;
            _adj[endpoint1].AddFirst(edge);
            _adj[endpoint2].AddFirst(edge);
            _edges++;
        }

        public IEnumerable<Edge> Adj(int v)
        {
            return _adj[v];
        }

        public List<Edge> Edges()
        {
            var list = new List<Edge>();
            
            for (int vertexIndex = 0; vertexIndex < Vertices; vertexIndex++)
            {
                int selfLoops = 0;

                foreach (var edge in Adj(vertexIndex))
                {
                    if (edge.Target(vertexIndex) > vertexIndex)
                    {
                        list.Add(edge);
                    }
                    else if (edge.Target(vertexIndex) == vertexIndex)
                    {
                        if (selfLoops % 2 == 0) 
                            list.Add(edge);
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