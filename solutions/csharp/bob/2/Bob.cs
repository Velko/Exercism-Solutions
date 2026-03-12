using System;
using System.Linq;

public static class Bob
{
    public static string Response(string statement)
    {
        statement = statement?.Trim();

        if (string.IsNullOrEmpty(statement))
            return "Fine. Be that way!";

        var is_question = statement[^1] == '?';
        var has_letters = statement.Any(char.IsLetter);
        var is_shouting = statement.ToUpper() == statement && has_letters;

        if (is_shouting)
        {
            if (is_question)
                return "Calm down, I know what I'm doing!";
            else
                return "Whoa, chill out!";
        }

        if (is_question)
            return "Sure.";
        
        return "Whatever.";
    }
}