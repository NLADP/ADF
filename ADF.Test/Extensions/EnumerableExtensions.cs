using System;
using System.Collections.Generic;
using System.Linq;

namespace Adf.Test.Extensions
{
    public static class EnumerableExtensions
    {
        public static T PickOne<T>(this IEnumerable<T> e)
        {
            var one = e.Skip(new Random().Next(e.Count()) - 1).First();

            Console.WriteLine("Picked {0}: {1}", typeof(T).Name, one);

            return one;
        }

        public static T PickOneOrDefault<T>(this IEnumerable<T> e, T defaultValue) where T : class
        {
            var one = e.Skip(new Random().Next(e.Count()) - 1).FirstOrDefault() ?? defaultValue;

            Console.WriteLine("Picked {0}: {1}", typeof(T).Name, one);

            return one;
        }
    }
}
