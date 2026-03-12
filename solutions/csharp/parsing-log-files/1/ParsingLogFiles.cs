using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class LogParser
{
    public bool IsValidLine(string text)
    {
        return Regex.IsMatch(text, @"^\[(TRC|DBG|INF|WRN|ERR|FTL)\]");
    }

    public string[] SplitLogLine(string text)
    {
        return Regex.Split(text, @"<[\^\*\=\-]*>");
    }

    public int CountQuotedPasswords(string lines)
    {
        return Regex.Count(lines, @"\"".*password.*\""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
    }

    public string RemoveEndOfLineText(string line)
    {
        return Regex.Replace(line, @"end-of-line\d*", "");
    }

    public string[] ListLinesWithPasswords(string[] lines)
    {
        var result = new List<string>();
        foreach (var line in lines) {
            var m = Regex.Match(line, @"(password\w+)", RegexOptions.IgnoreCase);
            if (m.Success) {
                result.Add($"{m.Groups[1]}: {line}");
            } else {
                result.Add($"--------: {line}");
            }
        }

        return result.ToArray();
    }
}
