using System;
using System.Text;

public static class Identifier
{
    public static string Clean(string identifier)
    {
        var sb = new StringBuilder();

        var upperNext = false;
        foreach (var c in identifier) {
            var (replaced, uNext) = c switch {
                ' ' => ("_", false),
                '\0' => ("CTRL", false),
                 > 'α' and <= 'ω' => ("", false),
                '-' => ("", true),                
                _  => (upperNext ? c.ToString().ToUpper() : c.ToString(), false),
            };

            sb.Append(replaced);
            upperNext = uNext;
        }

        return sb.ToString();
    }
}
