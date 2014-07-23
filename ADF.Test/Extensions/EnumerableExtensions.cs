using System;
using System.Collections.Generic;
using System.Linq;

namespace Adf.Test.Extensions
{
    public static class EnumerableExtensions
    {
        private static Random _random;
        private static Random Random
        {
            get { return _random ?? (_random = new Random()); }
        }

        public static T PickOne<T>(this IEnumerable<T> e)
        {
            var one = e.Skip(Random.Next(e.Count()) - 1).First();

            Console.WriteLine("Picked {0}: {1}", typeof(T).Name, one);

            return one;
        }

        public static T PickOneOrDefault<T>(this IEnumerable<T> e, T defaultValue) where T : class
        {
            var one = e.Skip(Random.Next(e.Count()) - 1).FirstOrDefault() ?? defaultValue;

            Console.WriteLine("Picked {0}: {1}", typeof(T).Name, one);

            return one;
        }
    }
}
