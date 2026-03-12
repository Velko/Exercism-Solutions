using System;
using System.Text;

public static class Identifier
{
    public static string Clean(string identifier)
    {
        var sb = new StringBuilder();

        var upperThis = false;
        foreach (var c in identifier) {
            var (replaced, upperNext) = c switch {
                _ when char.IsWhiteSpace(c) => ("_", false),
                _ when char.IsControl(c) => ("CTRL", false),
                _ when IsGreek(c) => ("", false),
                '-' => ("", true),                
                _ when char.IsLetter(c) => (upperThis ? c.ToString().ToUpper() : c.ToString(), false),
               _ => default,                
            };

            sb.Append(replaced);
            upperThis = upperNext;
        }

        return sb.ToString();
    }

    static bool IsGreek(char c) => c > 'α' && c <= 'ω';
}
