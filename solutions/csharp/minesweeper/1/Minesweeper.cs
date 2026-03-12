using System;
using System.Linq;
using System.Collections.Generic;

public static class Minesweeper
{
    public static string[] Annotate(string[] input)
    {
        var field = input.Select(r => r.ToCharArray()).ToArray();

        for (var y = 0; y < field.Length; ++y)
        {
            for (var x = 0; x < field[y].Length; ++x) {
                if (field[y][x] == '*')
                {
                    foreach (var (nx, ny) in CellsAround(x, y, field[y].Length, field.Length))
                    {
                        if (field[ny][nx] != '*')
                        {
                            field[ny][nx] = (char)(ZeroSubstitute(field[ny][nx]) + 1);
                        }
                    }
                }
            }
        }

        return field
            .Select(a => new string(a))
            .ToArray();
    }

    static IEnumerable<(int, int)> CellsAround(int x, int y, int width, int height)
    {
        var neighbours = new[] {(-1, -1), (0, -1), (1, -1),
                                (-1,  0),          (1,  0),
                                (-1,  1), (0,  1), (1,  1)};

        return neighbours
            .Select(n => (AddLimited(x, n.Item1, width), AddLimited(y, n.Item2, height)))
            .Where(a => a.Item1.HasValue && a.Item2.HasValue)
            .Select(n => (n.Item1.Value, n.Item2.Value));
    }

    static int? AddLimited(int value, int offset, int upper_limit)
    {
        var res = value + offset;

        if (res >= 0 && res < upper_limit)
            return res;

        return null;
    }

    static char ZeroSubstitute(char c)
        => c == ' ' ? '0' : c;

}
