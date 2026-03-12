using System;
using System.Collections.Generic;

public static class MatchingBrackets
{
    public static bool IsPaired(string input)
    {
        var expectedClosing = new Stack<char>();

        foreach (var c in input)
        {
            char closing;

            switch (c)
            {
                case '[':
                    expectedClosing.Push(']');
                    break;
                case '{':
                    expectedClosing.Push('}');
                    break;
                case '(':
                    expectedClosing.Push(')');
                    break;
                case ']':
                case '}':
                case ')':
                    if (!expectedClosing.TryPop(out closing) || closing != c)
                        return false;
                    break;
            }
        }

        return expectedClosing.Count == 0;
    }
}
