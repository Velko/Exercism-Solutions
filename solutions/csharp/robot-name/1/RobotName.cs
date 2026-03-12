using System;
using System.Linq;
using System.Collections.Generic;

public class Robot
{
    public Robot()
    {
        Reset();
    }
    public string Name {get; private set;}

    static Random rand = new Random();
    static HashSet<string> used_names = new HashSet<string>();

    public void Reset()
    {
        for(;;) {
            var candidate = string.Join("",
                rand.NextChars('A', 'Z').Take(2)
                .Concat(rand.NextChars('0', '9').Take(3))
            );

            if (used_names.Add(candidate))
            {
                Name = candidate;
                return;
            }
        }
    }
}

public static class RandomExt {

    public static IEnumerable<char> NextChars(this Random rand, char first, char last)
    {
        for (;;)
            yield return (char)rand.Next(first, last + 1);
    }
}