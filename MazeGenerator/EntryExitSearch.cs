using System;
using MazeGenerator.Utils;

namespace MazeGenerator
{
    public class EntryExitSearch
    {
        private readonly bool[,] _map;
        private readonly Random _random = new Random();
        private readonly int _mapWidth;
        private readonly int _mapHeight;

        private static bool _printInfo = true;
        
        public EntryExitSearch(bool[,] map)
        {
            _map = map;
            _mapWidth = _map.GetLength(0);
            _mapHeight = _map.GetLength(1);
        }
        
        public GridCoordinates[] GetEntryAndExit()
        {
            var gridCoordinates = new GridCoordinates[2];
            gridCoordinates[0] = GetEntry();
            gridCoordinates[1] = GetExit(gridCoordinates[0]);
            return gridCoordinates;
        }

        private GridCoordinates GetEntry()
        {
            bool isHorizontal = _random.Next(2) == 1;
            int startX;
            int startY;
            bool isValidEntry;
            do
            {
                if(_printInfo)
                    Console.WriteLine($"Entry is horizontal: {isHorizontal}");

                if (isHorizontal)
                {
                    startY = _random.Next(2) == 1 ? 0 : _mapHeight - 1;
                    startX = _random.Next(_mapWidth);
                }
                else
                {
                    startX = _random.Next(2) == 1 ? 0 : _mapWidth - 1;
                    startY = _random.Next(_mapHeight);
                }

                isValidEntry = IsValidEntry(startX, startY);
            } while (!isValidEntry);
            
            if(_printInfo)
                Console.WriteLine($"Entry: {startX},{startY}");

            return new GridCoordinates(startX, startY);
        }

        private GridCoordinates GetExit(GridCoordinates entry)
        {
            int startX = 0;
            int startY = 0;
            bool isHorizontal;
            if (entry.X == 0)
            {
                startX = _mapWidth - 1;
                isHorizontal = true;
            }
            else if (entry.X == _mapWidth - 1)
            {
                startX = 0;
                isHorizontal = true;
            }
            else if (entry.Y == 0)
            {
                startY = _mapHeight - 1;
                isHorizontal = false;
            }
            else if (entry.Y == _mapHeight - 1)
            {
                startY = 0;
                isHorizontal = false;
            }
            else
            {
                throw new Exception("Entry is not on the edge of the map");
            }

            bool isValidEntry;
            do
            {
                if (isHorizontal)
                {
                    startY = _random.Next(_mapHeight);
                }
                else
                {
                    startX = _random.Next(_mapWidth);
                }
                
                isValidEntry = IsValidEntry(startX, startY);
            } while (!isValidEntry);
            
            if(_printInfo)
                Console.WriteLine($"Entry: {startX},{startY}");
            
            return new GridCoordinates(startX, startY);
        }

        private bool IsValidEntry(int x, int y)
        {
            var newY = y - 1;
            var newX = x;
            if (newY > 0)
            {
                if (_map[newX, newY])
                {
                    return true;
                }
            }
            // check bottom
            newY = y + 1;
            newX = x;
            if (newY < _mapHeight)
            {
                if (_map[newX, newY])
                {
                    return true;
                }
            }
            // check left
            newY = y;
            newX = x - 1;
            if (newX > 0)
            {
                if (_map[newX, newY])
                {
                    return true;
                }
            }
            // check right
            newY = y;
            newX = x + 1;
            if (newX < _mapWidth)
            {
                if (_map[newX, newY])
                {
                    return true;
                }
            }

            if(_printInfo)
                Console.WriteLine($"Coordinates {x},{y} don't have access to the maze");
            return false;
        }
    }
}