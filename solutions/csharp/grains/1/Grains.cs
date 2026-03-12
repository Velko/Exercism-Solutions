using System;

public static class Grains
{
    public static ulong Square(int n)
    {
        if (!(n is >= 1 and <= 64))
            throw new ArgumentOutOfRangeException();

        return 1ul << (n - 1);
    }

    public static ulong Total()
        => UInt64.MaxValue;
}