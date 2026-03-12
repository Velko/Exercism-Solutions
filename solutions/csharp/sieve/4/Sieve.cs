// Incremental sieve
// Based on: https://www.cs.hmc.edu/~oneill/papers/Sieve-JFP.pdf

using System;
using System.Linq;
using System.Collections.Generic;

public static class Sieve
{
    public static int[] Primes(int limit)
    {
        if (limit < 0)
            throw new ArgumentOutOfRangeException();

        return IncrementalSieve()
            .TakeWhile(n => n <= limit)
            .ToArray();
    }

    private static IEnumerable<int> IncrementalSieve()
    {
        var composites_queue = new PriorityQueue<PrimeComposite, int>();

        int candidate = 2;

        for (;;)
        {
            bool is_prime = true;

            while (composites_queue.TryPeek(out var composite, out _ )
                    && composite.current_composite <= candidate)
            {
                _ = composites_queue.Dequeue();

                var next = composite.Next();
                composites_queue.Enqueue(next, next.current_composite);

                is_prime = false;
            }

            if (is_prime)
            {
                yield return candidate;

                /* While the algorithm itself poses no upper limit for the calculation,
                   the underlying data types do. We can not reach more than int.MaxValue,
                   so stop adding new Composites to list once Math.Sqrt(int.MaxValue) is
                   reached.

                   Past that, the multiplication will overflow and add invalid items,
                   leading to incorrect results.
                */
                if (candidate <= MAX_SQUARE)
                {
                    var next = PrimeComposite.New(candidate);
                    composites_queue.Enqueue(next, next.current_composite);
                }
            }
            ++candidate;
        }
    }

    static int MAX_SQUARE = (int)Math.Sqrt(int.MaxValue);

    struct PrimeComposite
    {
        public int current_composite;
        public int base_prime;

        public static PrimeComposite New(int prime)
            => new PrimeComposite
            {
                current_composite = prime * prime,
                base_prime = prime,
            };

        public PrimeComposite Next()
            => new PrimeComposite
            {
                current_composite = current_composite + base_prime,
                base_prime = base_prime,
            };
    }
}