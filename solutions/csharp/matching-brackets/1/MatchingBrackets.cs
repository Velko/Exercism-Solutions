using System;
using System.Collections.Generic;

public static class MatchingBrackets
{
    public static bool IsPaired(string input)
    {
        var stack = new Stack<char>();

        foreach (var c in input)
        {
            char opening;

            switch (c)
            {
                case '[':
                case '{':
                case '(':
                    stack.Push(c);
                    break;
                case ']':
                    if (!stack.TryPop(out opening) || opening != '[')
                        return false;
                    break;
                case '}':
                    if (!stack.TryPop(out opening) || opening != '{')
                        return false;
                    break;
                case ')':
                    if (!stack.TryPop(out opening) || opening != '(')
                        return false;
                    break;
            }
        }

        return stack.Count == 0;
    }
}
