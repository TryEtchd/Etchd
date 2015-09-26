using System;
using System.Collections.Generic;

namespace Etchd.Extensions
{
    public static class BclExtension
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach(T item in enumeration)
            {
                action(item);
            }
        }
    }
}