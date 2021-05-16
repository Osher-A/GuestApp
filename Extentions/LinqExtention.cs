using System;
using System.Collections.Generic;
using System.Linq;

namespace GuestApp.Services.Extentions
{
    public static class LinqExtention
    {
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
    }
}
