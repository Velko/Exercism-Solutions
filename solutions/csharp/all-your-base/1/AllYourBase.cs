using System;
using System.Collections.Generic;

public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
    {
        if (inputBase < 2) throw new ArgumentException();
        if (outputBase < 2) throw new ArgumentException();

        int number = 0;
        foreach (var inputDigit in inputDigits)
        {
            if (inputDigit < 0 || inputDigit >= inputBase) throw new ArgumentException();
            number *= inputBase;
            number += inputDigit;
        }

        if (number == 0)
            return new[] { 0 };

        var outputDigits = new List<int>();
        while (number > 0)
        {
            outputDigits.Add(number % outputBase);
            number /= outputBase;
        }

        outputDigits.Reverse();

        return outputDigits.ToArray();
    }
}