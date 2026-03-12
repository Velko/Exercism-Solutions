using System;
using System.Linq;

public static class Sieve
{
    public static int[] Primes(int limit)
    {
        if (limit < 0)
            throw new ArgumentOutOfRangeException();

        var sieve = Enumerable.Repeat(true, limit + 1).ToArray();
        sieve[0] = sieve[1] = false;

        for (int p = 2; p <= limit; ++p)
        {
            if (sieve[p])
            {
                for (int m = p * p; m <= limit; m += p)
                    sieve[m] = false;
            }
        }

        return sieve
                .Select((b, i) => (b, i))
                .Where(bi => bi.b)
                .Select(bi => bi.i)
                .ToArray();
    }
}