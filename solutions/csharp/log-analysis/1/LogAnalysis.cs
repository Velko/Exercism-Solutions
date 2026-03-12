using System;

public static class LogAnalysis
{
    public static string SubstringAfter(this string str, string delimiter)
    {
        var delimiterIndex = str.IndexOf(delimiter);

        return str.Substring(delimiterIndex + delimiter.Length);
    }

    public static string SubstringBetween(this string str, string start, string end)
    {
        var strAfter = str.SubstringAfter(start);

        var delimiterIndex = strAfter.IndexOf(end);

        return strAfter.Substring(0, delimiterIndex);
    }

    public static string Message(this string logLine)
    {
        return logLine.SubstringAfter("]: ");
    }

    public static string LogLevel(this string logLine)
    {
        return logLine.SubstringBetween("[", "]");
    }
}