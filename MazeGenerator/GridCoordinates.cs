namespace MazeGenerator
{
    public struct GridCoordinates
    {
        public int X { get; }
        public int Y { get; }

        public GridCoordinates(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
    }
}