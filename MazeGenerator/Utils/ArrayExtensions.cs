using System;
using System.Collections.Generic;

namespace MazeGenerator.Utils
{
    public static class ArrayExtensions
    {
#if SET_SEED
        private static readonly Random _random = new Random(0);
#else
        private static readonly Random _random = new Random();
#endif

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static void Shuffle<T>(this T[] list)
        {
            int n = list.Length;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T GetRandom<T>(this IList<T> list)
        {
            return list[_random.Next(list.Count)];
        }
    }
}