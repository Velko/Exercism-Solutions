using System;
using System.Collections.Generic;
using System.Linq;

public static class ProteinTranslation
{
    public static string[] Proteins(string strand)
    {
        return StrandIter(strand)
            .TakeWhile(x => x != null)
            .ToArray();
    }

    private static IEnumerable<string> StrandIter(string strand)
    {
        for (int i = 0; i < strand.Length; i += 3)
        {
            yield return strand.Substring(i, 3) switch
            {
                "AUG" => "Methionine",
                "UUU" or "UUC" => "Phenylalanine",
                "UUA" or "UUG" => "Leucine",
                "UCU" or "UCC" or "UCA" or "UCG" => "Serine",
                "UAU" or "UAC"                   => "Tyrosine",
                "UGU" or "UGC"                   => "Cysteine",
                "UGG"                            => "Tryptophan",
                "UAA" or "UAG" or "UGA"          => null,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}