using System;
using System.Text;

public static class RotationalCipher
{
    public static string Rotate(string text, int shiftKey)
    {
        var sb = new StringBuilder();

        foreach (var c in text)
        {
            sb.Append(c switch {
                >= 'a' and <= 'z' => RotateChar(c, 'a', shiftKey),
                >= 'A' and <= 'Z' => RotateChar(c, 'A', shiftKey),
                _ => c,
            });
        }

        return sb.ToString();
    }

    private static char RotateChar(char c, char range_start, int shiftKey)
    {
        const int MODULO = 'Z' - 'A' + 1;
        var offset = c - range_start;

        return (char)(range_start + (offset + shiftKey) % MODULO);
    }
}