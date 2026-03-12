using System;

public static class BinarySearch
{
    public static int Find(int[] input, int value)
    {
        var lo = 0;
        var hi = input.Length;

        while (lo < hi)
        {
            var i = (lo + hi) / 2;
            var cmp = value - input[i];

            if (cmp > 0)
                lo = i + 1;
            else if (cmp < 0)
                hi = i;
            else
                return i;
        }

        return -1;
    }
}