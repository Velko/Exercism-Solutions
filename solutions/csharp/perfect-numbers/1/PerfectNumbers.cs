using System;

public enum Classification
{
    Perfect,
    Abundant,
    Deficient
}

public static class PerfectNumbers
{
    public static Classification Classify(int number)
    {
        if (number < 1)
            throw new ArgumentOutOfRangeException();

        // edge case
        if (number == 1)
            return Classification.Deficient;

        var root = (int)Math.Sqrt(number);

        var aliquot_sum = 1; // we're starting with 2, but 1 is a valid factor
        for (var factor = 2; factor <= root; ++factor)
        {
            if (number % factor == 0)
            {
                aliquot_sum += factor;

                var complementary = number / factor;
                if (complementary != factor)
                    aliquot_sum += complementary;
            }
        }

        if (aliquot_sum == number)
            return Classification.Perfect;

        if (aliquot_sum > number)
            return Classification.Abundant;

        return Classification.Deficient;
    }
}
