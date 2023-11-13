using System;

namespace MazeGenerator.Utils
{
    public readonly struct GridCoordinates
    {
        public int X { get; }
        public int Y { get; }

        public GridCoordinates(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
        
        public bool Equals(GridCoordinates other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is GridCoordinates other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}