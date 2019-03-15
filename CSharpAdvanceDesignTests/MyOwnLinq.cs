using System;
using System.Collections.Generic;

static internal class MyOwnLinq
{
    public static List<TSource> JoeyWhere<TSource>(List<TSource> sources, Func<TSource, bool> predicate)
    {
        var list = new List<TSource>();
        foreach (var source in sources)
        {
            if (predicate(source))
            {
                list.Add(source);
            }
        }
        return list;
    }
}