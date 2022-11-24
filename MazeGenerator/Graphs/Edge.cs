using System;

namespace MazeGenerator.Graphs
{
    public class Edge : IComparable<Edge>
    {
        public int Endpoint1 { get; }
        public int Endpoint2 { get; }
        public float Weight { get; }

        public Edge(int endpoint1, int endpoint2, float weight)
        {
            Endpoint1 = endpoint1;
            Endpoint2 = endpoint2;
            Weight = weight;
        }

        public int CompareTo(Edge other)
        {
            if (Weight < other.Weight)
                return -1;
            if (Weight > other.Weight)
                return +1;
            return 0;
        }

        public int Target(int vertex)
        {
            if (vertex == Endpoint1) return Endpoint2;
            if (vertex == Endpoint2) return Endpoint1;
            throw new Exception("Illegal endpoint");
        }

        public override string ToString()
        {
            return $"{Endpoint1:d}-{Endpoint2:d}";// {Weight:f5}";
        }
    }
}