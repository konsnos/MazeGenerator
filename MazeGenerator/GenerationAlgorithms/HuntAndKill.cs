using System;
using System.Collections.Generic;
using MazeGenerator.Utils;

namespace MazeGenerator.GenerationAlgorithms
{
    // Implementation instructions from http://weblog.jamisbuck.org/2011/1/24/maze-generation-hunt-and-kill-algorithm
    /// <summary>
    /// The random walk phase tends to produce long,
    /// windy passages that are reminiscent of the recursive backtracking algorithm.
    /// Both algorithms produce mazes with fewer dead-ends than most of the other algorithms.
    /// </summary>
    public class HuntAndKill : IMap
    {
        public int[,] Grid { get; }
        private readonly int _width;
        private readonly int _height;

        public HuntAndKill(int gridWidth, int gridHeight)
        {
            _width = gridWidth;
            _height = gridHeight;

            Grid = new int[_width, _height];
            CreateMap();
        }

        private void CreateMap()
        {
            var random = new Random();
            int x = random.Next(0, _width);
            int y = random.Next(0, _height);

            bool stillSearching;
            do
            {
                Walk(x, y);
                stillSearching = Hunt(ref x, ref y);
            } while (stillSearching);
        }

        private void Walk(int x, int y)
        {
            bool walking;
            do
            {
                walking = WalkTile(x, y, ref x, ref y);
            } while (walking);
        }

        private bool WalkTile(int fromX, int fromY, ref int toX, ref int toY)
        {
            var directions = new Direction[]
                { Direction.Top, Direction.Bottom, Direction.Right, Direction.Left };
            directions.Shuffle();

            foreach (var direction in directions)
            {
                toX = fromX + direction.GetX();
                if (toX < 0 || toX >= _width) continue;
                toY = fromY + direction.GetY();
                if (toY < 0 || toY >= _height) continue;

                if (Grid[toX, toY] != 0) continue;

                Grid[fromX, fromY] |= (int)direction;
                Grid[toX, toY] |= (int)direction.Opposite();

                return true;
            }

            return false;
        }

        private bool Hunt(ref int fromX, ref int fromY)
        {
            var directionsAvailable = new List<Direction>(Enum.GetNames(typeof(Direction)).Length);

            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    if (Grid[column, row] != 0) continue;

                    directionsAvailable.Clear();

                    if (row > 0 && Grid[column, row + Direction.Top.GetY()] != 0)
                    {
                        directionsAvailable.Add(Direction.Top);
                    }

                    if (column > 0 && Grid[column + Direction.Left.GetX(), row] != 0)
                    {
                        directionsAvailable.Add(Direction.Left);
                    }

                    if (row + 1 < _height && Grid[column, row + Direction.Bottom.GetY()] != 0)
                    {
                        directionsAvailable.Add(Direction.Bottom);
                    }

                    if (column + 1 < _width && Grid[column + Direction.Right.GetX(), row] != 0)
                    {
                        directionsAvailable.Add(Direction.Right);
                    }

                    if (directionsAvailable.Count == 0) continue;

                    var direction = directionsAvailable.GetRandom();

                    fromX = column;
                    fromY = row;
                    int toX = fromX + direction.GetX();
                    int toY = fromY + direction.GetY();

                    Grid[fromX, fromY] |= (int)direction;
                    Grid[toX, toY] |= (int)direction.Opposite();

                    return true;
                }
            }

            return false;
        }

        public bool[,] GetMap()
        {
            int mapWidth = (_width * 2) + 1;
            int mapHeight = (_height * 2) + 1;

            var map = new bool[mapWidth, mapHeight];
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int mapX = (x * 2) + 1;
                    int mapY = (y * 2) + 1;
                    if (Grid[x, y] != 0)
                    {
                        map[mapX, mapY] = true;
                    }

                    if ((Grid[x, y] & (int)Direction.Top) != 0)
                    {
                        map[mapX + Direction.Top.GetX(), mapY + Direction.Top.GetY()] = true;
                    }

                    if ((Grid[x, y] & (int)Direction.Left) != 0)
                    {
                        map[mapX + Direction.Left.GetX(), mapY + Direction.Left.GetY()] = true;
                    }
                }
            }

            return map;
        }
    }
}