using System;
using System.Collections.Generic;

namespace MazeGenerator.Utils
{
    public class GridCoordinatesParent
    {
        public int X { get; }
        public int Y { get; }

        private GridCoordinatesParent Parent { get; }
        
        public GridCoordinatesParent(int newX, int newY, GridCoordinatesParent parent)
        {
            X = newX;
            Y = newY;
            Parent = parent;
        }

        public GridCoordinatesParent(GridCoordinates coordinates)
        {
            X = coordinates.X;
            Y = coordinates.Y;
        }

        public List<GridCoordinates> GetPath()
        {
            var list = new List<GridCoordinates>();

            var current = this;

            while (current != null)
            {
                list.Add(current.ToGridCoordinates());
                current = current.Parent;
            }

            return list;
        }

        public GridCoordinates ToGridCoordinates()
        {
            return new GridCoordinates(X, Y);
        }
        
        public bool Equals(GridCoordinatesParent other)
        {
            return X == other.X && Y == other.Y;
        }

        public bool Equals(GridCoordinates other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() == typeof(GridCoordinatesParent)) return Equals((GridCoordinatesParent)obj);
            if (obj.GetType() == typeof(GridCoordinates)) return Equals((GridCoordinates)obj);
            return false;
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