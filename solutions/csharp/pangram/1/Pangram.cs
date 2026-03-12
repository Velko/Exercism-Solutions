using System;
using System.Collections.Generic;
using System.Linq;

public static class Pangram
{
    static HashSet<char> Alphabet = new HashSet<char>(
        Enumerable.Range('a', 'z'-'a' + 1).Select(c => (char)c)
    );

    public static bool IsPangram(string input)
    {
        var charsInInput = new HashSet<char>(
                input
                    .ToLower()
                    .Where(Alphabet.Contains)
                );

        return charsInInput.SetEquals(Alphabet);
    }
}
