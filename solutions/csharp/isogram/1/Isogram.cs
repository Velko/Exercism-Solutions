using System;
using System.Linq;

public static class Isogram
{
    public static bool IsIsogram(string word)
    {
        var wordLetters = word
            .ToLower()
            .Where(char.IsAsciiLetter)
            .ToList();

        var uniqueLetters = wordLetters.ToHashSet();

        return wordLetters.Count == uniqueLetters.Count;
    }
}
