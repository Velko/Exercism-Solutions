using System;
using System.Collections.Generic;
using Sprache;
using System.Linq;

public static class Alphametics
{
    private static readonly Parser<char> SeparatorChar = Parse.Chars(" \t");
    private static readonly Parser<string> EqualSign = Parse.String("==").Text();
    private static readonly Parser<char> PlusSign = Parse.Char('+');
    private static readonly Parser<string> Word = Parse.Letter.AtLeastOnce().Text();

    private static readonly Parser<char> ListDelimiter =
        from _sep0 in SeparatorChar.Many()
        from op in PlusSign
        from _sep1 in SeparatorChar.Many()
        select op;

    private static readonly Parser<string> ResultDelimiter =
        from _sep0 in SeparatorChar.Many()
        from op in EqualSign
        from _sep1 in SeparatorChar.Many()
        select op;

    private static readonly Parser<string[]> SumExpression =
        from words in Word.DelimitedBy(ListDelimiter)
        select words.ToArray();

    private static readonly Parser<ParsedEquation> Equation =
        from sumexpr in SumExpression
        from _ in ResultDelimiter
        from res in Word
        select new ParsedEquation {
            args = sumexpr,
            result = res,
        };

    class ParsedEquation
    {
        public string[] args;
        public string result;
    }

    class CharSubstitutionVariants
    {
        public char Char { get; set; }
        public HashSet<int> PossibleDigits { get; set; }
    }

    public static IDictionary<char, int> Solve(string equation)
    {

        var parsedEquation = Equation.Parse(equation);

        var allDigits = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        List<CharSubstitutionVariants> chars =
            parsedEquation.args
                .SelectMany(c => c)
                .Union(parsedEquation.result)
                .Distinct()
                .Select(c => new CharSubstitutionVariants { Char = c, PossibleDigits = new HashSet<int>(allDigits)})
                .ToList();

        // most significant digits can not be zero
        foreach (var nonZero in parsedEquation.args.Concat(new[] { parsedEquation.result }))
        {
            if (chars.FirstOrDefault(c => c.Char == nonZero[0]) is CharSubstitutionVariants nzChar)
            {
                nzChar.PossibleDigits.Remove(0);
            }
        }

        // mark if only single digit can be zero
        if (chars.Count(p => p.PossibleDigits.Contains(0)) == 1)
        {
            chars.Single(p => p.PossibleDigits.Contains(0)).PossibleDigits = new HashSet<int>{ 0 };
        }


        var charMapping = BruteforceSubstitutionsRecursive(parsedEquation, chars, new List<int>());

        if (charMapping == null)
            throw new ArgumentException();

        return charMapping;
    }

    static long WordToNumber(string word, IDictionary<char, int> substitutions)
    {
        long number = 0;
        long multiplier = 1;

        foreach (var c in word.Reverse())
        {
            number += substitutions[c] * multiplier;
            multiplier *= 10;
        }

        return number;
    }

    static IDictionary<char, int> BruteforceSubstitutionsRecursive(ParsedEquation equation, List<CharSubstitutionVariants> keys, List<int> values)
    {
        if (values.Count < keys.Count)
        {
            foreach (int i in keys[values.Count].PossibleDigits)
            {
                if (values.Contains(i))
                    continue;

                var subValues = values.Concat( new[] {i}).ToList();
                var substitutions = BruteforceSubstitutionsRecursive(equation, keys, subValues);

                if (substitutions != null)
                    return substitutions;
            }
        } else {
            var substitutions = keys.Zip(values).ToDictionary(k => k.First.Char, v => v.Second);

            if (CheckSubstitution(equation, substitutions))
                return substitutions;
        }

        return null;
    }

    static bool CheckSubstitution(ParsedEquation equation, IDictionary<char, int> substitutions)
        => equation.args.Select(w => WordToNumber(w, substitutions)).Sum() == WordToNumber(equation.result, substitutions)
            && substitutions[equation.result[0]] != 0
            && !equation.args.Any(w => substitutions[w[0]] == 0);

}
