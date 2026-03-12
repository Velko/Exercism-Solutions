using System;
using System.Linq;

public static class Proverb
{
    public static string[] Recite(string[] subjects)
        => subjects
            .Zip(subjects.Skip(1))
            .Select(pair => $"For want of a {pair.Item1} the {pair.Item2} was lost.")
            .Concat(subjects
                        .Take(1)
                        .Select(subject => $"And all for the want of a {subject}.")
                    )
            .ToArray();
}