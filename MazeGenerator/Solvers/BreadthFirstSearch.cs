using System.Collections.Generic;
using MazeGenerator.Utils;

namespace MazeGenerator.Solvers
{
    public class BreadthFirstSearch
    {
        private readonly bool[,] _map;
        private readonly int _mapHeight;
        private readonly int _mapWidth;
        
        private readonly GridCoordinates _entry;
        private readonly GridCoordinates _exit;
        
        public BreadthFirstSearch(bool[,] map, GridCoordinates entry, GridCoordinates exit)
        {
            _map = map;
            _mapHeight = map.GetLength(1);
            _mapWidth = map.GetLength(0);
            
            _entry = entry;
            _exit = exit;
        }
        
        public GridCoordinatesParent GetPath()
        {
            var explored = new bool[_mapWidth, _mapHeight];
            // set explored to false
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    explored[x, y] = false;
                }
            }
            
            var queue = new Queue<GridCoordinatesParent>();
            queue.Enqueue(new GridCoordinatesParent(_entry));
            
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                explored[current.X, current.Y] = true;
                
                if (current.Equals(_exit))
                {
                    // found exit
                    return current;
                }

                var neighbours = GetNeighbours(current);
                foreach (var neighbour in neighbours)
                {
                    if (!explored[neighbour.X, neighbour.Y])
                    {
                        queue.Enqueue(neighbour);
                    }
                }
            }
            
            // no exit found
            return null;
        }
        
        private IEnumerable<GridCoordinatesParent> GetNeighbours(GridCoordinatesParent current)
        {
            var neighbours = new List<GridCoordinatesParent>();
            
            // left
            if (current.X > 0 && _map[current.X - 1, current.Y])
            {
                neighbours.Add(new GridCoordinatesParent(current.X - 1, current.Y, current));
            }
            
            // right
            if (current.X < _mapWidth - 1 && _map[current.X + 1, current.Y])
            {
                neighbours.Add(new GridCoordinatesParent(current.X + 1, current.Y, current));
            }
            
            // up
            if (current.Y > 0 && _map[current.X, current.Y - 1])
            {
                neighbours.Add(new GridCoordinatesParent(current.X, current.Y - 1, current));
            }
            
            // down
            if (current.Y < _mapHeight - 1 && _map[current.X, current.Y + 1])
            {
                neighbours.Add(new GridCoordinatesParent(current.X, current.Y + 1, current));
            }

            return neighbours;
        }
    }
}