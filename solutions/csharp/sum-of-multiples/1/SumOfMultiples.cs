using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
        => multiples
            .Where(f => f > 0)
            .SelectMany(m =>
                Enumerable.Range(1, int.MaxValue)
                .Select(n => n * m)
                .TakeWhile(p => p < max)
            )
            .Distinct()
            .Sum();
}