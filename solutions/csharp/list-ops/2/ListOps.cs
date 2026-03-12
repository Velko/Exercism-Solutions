using System;
using System.Collections.Generic;

public static class ListOps
{
    public static int Length<T>(List<T> input)
        => Foldl(input, 0, (s, _) => s + 1);

    public static List<T> Reverse<T>(List<T> input)
        => Foldl(input, new List<T>(), (acc, item) => Append(new List<T>{item }, acc));

    public static List<TOut> Map<TIn, TOut>(List<TIn> input, Func<TIn, TOut> map)
    {
        var res = new List<TOut>();

        foreach (var item in input)
            res.Add(map(item));

        return res;
    }

    public static List<T> Filter<T>(List<T> input, Func<T, bool> predicate)
    {
        var res = new List<T>();

        foreach (var item in input)
            if (predicate(item))
                res.Add(item);

        return res;
    }

    public static TOut Foldl<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func)
    {
        var acc = start;

        foreach (var i in input)
            acc = func(acc, i);

        return acc;
    }

    public static TOut Foldr<TIn, TOut>(List<TIn> input, TOut start, Func<TIn, TOut, TOut> func)
        => Foldl(Reverse(input), start, (item, acc) => func(acc, item));

    public static List<T> Concat<T>(List<List<T>> input)
        => Foldl(input, new List<T>(), Append);

    public static List<T> Append<T>(List<T> left, List<T> right)
    {
        var res = new List<T>();

        foreach (var item in left)
            res.Add(item);

        foreach (var item in right)
            res.Add(item);

        return res;
    }
}