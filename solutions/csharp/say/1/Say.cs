using System;

public static class Say
{
    static readonly string[] ones_names = new [] {null, "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    static readonly string[] teens_names = new [] {null, "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
    static readonly string[] tens_names = new [] {null, "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
    public static string InEnglish(long number)
    {
        if (number < 0 || number >= 1_000_000_000_000)
            throw new ArgumentOutOfRangeException();

        string s = string.Empty;
        s+= ConvertThousandUnits(ref number, 1_000_000_000, "billion");
        s+= ConvertThousandUnits(ref number, 1_000_000, "million");
        s+= ConvertThousandUnits(ref number, 1_000, "thousand");
        s += ConvertHundredsTensOnes(number);

        if (number == 0 && s == "")
            return "zero";

        return s.TrimEnd();
    }

    private static string ConvertThousandUnits(ref long number, long multiplier, string units)
    {
        string result = string.Empty;

        if (number >= multiplier)
        {
            result = ConvertHundredsTensOnes(number / multiplier) + $" {units} ";
            number %= multiplier;
        }

        return result;
    }

    private static string ConvertHundredsTensOnes(long number)
    {
        string s = "";

        if (number >= 100)
        {
            s += $"{ones_names[number / 100]} hundred ";
            number %= 100;
        }

        if (number >= 20)
        {
            s += tens_names[number / 10];
            number %= 10;
        }

        if (number >= 10)
        {
            return teens_names[number - 10];
        }

        if (number > 0)
        {
            if (s != "")
                s += $"-{ones_names[number]}";
            else
                s = ones_names[number];
        }

        return s.TrimEnd();
    }
}