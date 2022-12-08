using System;

namespace MazeGenerator.Graphs
{
    public class Edge
    {
        public int Endpoint1 { get; }
        public int Endpoint2 { get; }

        public Edge(int endpoint1, int endpoint2)
        {
            Endpoint1 = endpoint1;
            Endpoint2 = endpoint2;
        }

        public int Target(int vertex)
        {
            if (vertex == Endpoint1) return Endpoint2;
            if (vertex == Endpoint2) return Endpoint1;
            throw new Exception("Illegal endpoint");
        }

        public override string ToString()
        {
            return $"{Endpoint1:d}-{Endpoint2:d}";
        }

        public static int ComparisonByVertex(Edge x, Edge y) => x.Endpoint1.CompareTo(y.Endpoint1);
    }
}