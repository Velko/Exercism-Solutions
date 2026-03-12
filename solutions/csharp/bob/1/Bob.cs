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

        if (is_question)
        {
            if (is_shouting)
                return "Calm down, I know what I'm doing!";

            return "Sure.";
        }

        if (is_shouting)
            return "Whoa, chill out!";

        return "Whatever.";
    }
}