using System;
using System.Text.RegularExpressions;
using System.Text;

public static class Acronym
{
    public static string Abbreviate(string phrase)
    {
        var sb = new StringBuilder();

        foreach (var m in Regex.EnumerateMatches(phrase, @"([A-Za-z']+)"))
        {
            sb.Append(phrase[m.Index]);
        }

        return sb.ToString().ToUpper();
    }
}