using System;
using MazeGenerator.GenerationAlgorithms;

namespace MazeGenerator.Utils
{
    public static class EnumExtensions
    {
        public static Direction Opposite(this Direction direction)
        {
            return direction switch
            {
                Direction.Top => Direction.Bottom,
                Direction.Bottom => Direction.Top,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        
        public static int GetX(this Direction direction)
        {
            return direction switch
            {
                Direction.Top => 0,
                Direction.Bottom => 0,
                Direction.Right => 1,
                Direction.Left => -1,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static int GetY(this Direction direction)
        {
            return direction switch
            {
                Direction.Top => -1,
                Direction.Bottom => 1,
                Direction.Right => 0,
                Direction.Left => 0,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}