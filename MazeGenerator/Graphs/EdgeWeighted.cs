using System;

namespace MazeGenerator.Graphs
{
    public class EdgeWeighted : Edge, IComparable<EdgeWeighted>
    {
        public float Weight { get; }

        public EdgeWeighted(int endpoint1, int endpoint2, float weight) : base(endpoint1, endpoint2)
        {
            Weight = weight;
        }

        public int CompareTo(EdgeWeighted other)
        {
            return Weight.CompareTo(other.Weight);
        }

        public override string ToString()
        {
            return $"{Endpoint1:d}-{Endpoint2:d} {Weight:f5}";
        }
    }
}