using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class ParallelLetterFrequency
{
    public static Dictionary<char, int> Calculate(IEnumerable<string> texts)
    {
        var chars = new ThreadSafeCharQueueAdapter(texts);

        var tasks = Enumerable.Range(0, 10)
            .Select(_ => Task.Run(() => CountCharacters(chars)))
            .ToList();

        var results = Task.WhenAll(tasks).GetAwaiter().GetResult();

        return results
            .SelectMany(r => r)
            .GroupBy(k => k.Key, v => v.Value)
            .ToDictionary(k => k.Key, v => v.Sum());
    }

    static IEnumerable<KeyValuePair<char, int>> CountCharacters(ThreadSafeCharQueueAdapter chars)
    {
        Dictionary<char, int> counts = new();

        while (chars.TryTake(out char c))
        {
            if (!Char.IsLetter(c))
                continue;

            c = Char.ToLower(c);

            int count = counts.GetValueOrDefault(c);
            counts[c] = count + 1;
        }

        return counts;
    }

    class ThreadSafeCharQueueAdapter
    {
        IEnumerator<char> enumerator;

        public ThreadSafeCharQueueAdapter(IEnumerable<string> texts)
        {
            var chars = texts.SelectMany(s => s);
            enumerator = chars.GetEnumerator();
        }

        public bool TryTake(out char c)
        {
            lock (this)
            {
                if (enumerator.MoveNext())
                {
                    c = enumerator.Current;
                    return true;
                }
            }

            c = '\0';
            return false;
        }
    }
}
