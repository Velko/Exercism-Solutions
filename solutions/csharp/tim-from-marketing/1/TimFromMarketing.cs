using System;
using System.Collections.Generic;

static class Badge
{
    public static string Print(int? id, string name, string? department)
    {
        List<string> parts = new();

        if (id.HasValue)
            parts.Add($"[{id}]");

        parts.Add(name);
        parts.Add(department?.ToUpper() ?? "OWNER" );

        return string.Join(" - ", parts);
    }
}
