using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShawnMclean.Utility.Collections
{
    public static class Filters
    {
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int pageNumber, int pageSize)
        {
            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
