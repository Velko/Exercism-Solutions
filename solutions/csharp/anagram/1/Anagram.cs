using System;
using System.Linq;

public class Anagram
{
    string lowerBase;
    string sortedBase;

    public Anagram(string baseWord)
    {
        lowerBase = baseWord.ToLower();
        sortedBase = SortCharsInWord(lowerBase);
    }

    public string[] FindAnagrams(string[] potentialMatches)
        => potentialMatches
            .Where(n => {
                var lowerCandidate = n.ToLower();
                return SortCharsInWord(lowerCandidate) == sortedBase
                    && lowerCandidate != lowerBase;
            })
            .ToArray();

    private static string SortCharsInWord(string word)
        => new string(word
                        .ToCharArray()
                        .Order()
                        .ToArray()
        );
}