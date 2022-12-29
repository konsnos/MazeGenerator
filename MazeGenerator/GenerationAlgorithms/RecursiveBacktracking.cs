using MazeGenerator.Utils;

namespace MazeGenerator.GenerationAlgorithms
{
    public class RecursiveBacktracking : IHasMap
    {
        public int[,] Grid { get; }
        private readonly int _width;
        private readonly int _height;

        public RecursiveBacktracking(int gridWidth, int gridHeight)
        {
            _width = gridWidth;
            _height = gridHeight;

            Grid = new int[_width, _height];
            GeneratePaths(0, 0);
        }

        private void GeneratePaths(int fromX, int fromY)
        {
            var directions = new Direction[]
                { Direction.Top, Direction.Bottom, Direction.Right, Direction.Left };
            directions.Shuffle();

            foreach (var direction in directions)
            {
                var toX = fromX + direction.GetX();
                if (toX < 0 || toX >= _width) continue;
                var toY = fromY + direction.GetY();
                if (toY < 0 || toY >= _height) continue;

                if (Grid[toX, toY] != 0) continue;

                Grid[fromX, fromY] |= (int)direction;
                Grid[toX, toY] |= (int)direction.Opposite();
                GeneratePaths(toX, toY);
            }
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