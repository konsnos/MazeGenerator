using System;
using System.Collections.Generic;
using System.Text;
using MazeGenerator.GenerationAlgorithms;
using MazeGenerator.Utils;

namespace MazeGenerator
{
    public class Mazes
    {
        public static bool[,] GetKruskal(int width, int height)
        {
            var kruskal = new KruskalRandom(width, height);
            return kruskal.GetMap();
        }

        public static bool[,] GetKruskalWithPassingBias(int width, int height,
            KruskalWeighted.BiasDirection biasDirection,
            float biasRatio)
        {
            var kruskal = new KruskalWeighted(width, height, biasDirection, biasRatio);
            return kruskal.GetMap();
        }

        public static bool[,] GetRecursiveBacktracking(int width, int height)
        {
            var recursiveBacktracking = new RecursiveBacktracking(width, height);
            return recursiveBacktracking.GetMap();
        }

        public static bool[,] GetHuntAndKill(int width, int height)
        {
            var huntAndKill = new HuntAndKill(width, height);
            return huntAndKill.GetMap();
        }

        public static void PrintMap(bool[,] map)
        {
            var mapHeight = map.GetLength(1);
            var mapWidth = map.GetLength(0);

            var stringBuilder = new StringBuilder();

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    stringBuilder.Append(map[x, y] ? "  " : "# ");
                }

                stringBuilder.Append(Environment.NewLine);
            }

            Console.Write(stringBuilder);
        }

        public static void PrintMap(bool[,] map, GridCoordinates entry, GridCoordinates exit)
        {
            var mapHeight = map.GetLength(1);
            var mapWidth = map.GetLength(0);

            var stringBuilder = new StringBuilder();

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (x == entry.X && y == entry.Y)
                    {
                        stringBuilder.Append("S ");
                    }
                    else if(x == exit.X && y == exit.Y)
                    {
                        stringBuilder.Append("X ");
                    }
                    else
                    {
                        stringBuilder.Append(map[x, y] ? "  " : "# ");
                    }
                }

                stringBuilder.Append(Environment.NewLine);
            }

            Console.Write(stringBuilder);
        }

        public static void PrintMap(bool[,] map, GridCoordinates entry, GridCoordinates exit, List<GridCoordinates> path)
        {
            var mapHeight = map.GetLength(1);
            var mapWidth = map.GetLength(0);

            var stringBuilder = new StringBuilder();

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (x == entry.X && y == entry.Y)
                    {
                        stringBuilder.Append("S ");
                    }
                    else if(x == exit.X && y == exit.Y)
                    {
                        stringBuilder.Append("X ");
                    }
                    else
                    {
                        if (path.Contains(new GridCoordinates(x, y)))
                        {
                            stringBuilder.Append("o ");
                        }
                        else
                        {
                            stringBuilder.Append(map[x, y] ? "  " : "# ");
                        }
                    }
                }

                stringBuilder.Append(Environment.NewLine);
            }

            Console.Write(stringBuilder);
        }
    }
}