using System;

static class LogLine
{
    public static string Message(string logLine)
    {
        var parts = logLine.Split(':');
        return parts[1].Trim();
    }

    public static string LogLevel(string logLine)
    {
        var parts = logLine.Split(':');
        return parts[0].Trim('[', ']').ToLower();
    }

    public static string Reformat(string logLine)
    {
        return $"{Message(logLine)} ({LogLevel(logLine)})";
    }
}
