using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGenerator.Graphs
{
    public class Graph
    {
        public int Vertices { get; }
        protected int _edges;
        protected LinkedList<Edge>[] _adj;

        public Graph(int vertices)
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

        public virtual void AddEdge(int endpoint1, int endpoint2)
        {
            var edge = new Edge(endpoint1, endpoint2);
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

        protected IEnumerable<Edge> Adj(int v)
        {
            return _adj[v];
        }

        public List<Edge> GetEdges()
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