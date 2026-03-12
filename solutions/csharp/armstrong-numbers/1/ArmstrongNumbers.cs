using System;
using System.Linq;

public static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number)
    {
        var num_str = number.ToString();

        var total = num_str
            .Select(c => (int)Math.Pow(c - '0', num_str.Length))
            .Sum();

        return total == number;
    }
}