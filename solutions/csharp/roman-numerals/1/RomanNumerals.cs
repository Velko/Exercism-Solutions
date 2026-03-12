using System;
using System.Text;

struct RomanDigit
{
    public int Value;
    public string Digit;
}




public static class RomanNumeralExtension
{
    static readonly RomanDigit[] RomanDigits = new [] {
        new RomanDigit { Value = 1000, Digit = "M" },
        new RomanDigit { Value = 900, Digit = "CM" },
        new RomanDigit { Value = 500, Digit = "D" },
        new RomanDigit { Value = 400, Digit = "CD" },
        new RomanDigit { Value = 100, Digit = "C" },
        new RomanDigit { Value = 90, Digit = "XC" },
        new RomanDigit { Value = 50, Digit = "L" },
        new RomanDigit { Value = 40, Digit = "XL" },
        new RomanDigit { Value = 10, Digit = "X" },
        new RomanDigit { Value = 9, Digit = "IX" },
        new RomanDigit { Value = 5, Digit = "V" },
        new RomanDigit { Value = 4, Digit = "IV" },
        new RomanDigit { Value = 1, Digit = "I" },
    };

    public static string ToRoman(this int value)
    {
        var sb = new StringBuilder();

        foreach (var roman in RomanDigits)
        {
            while (value >= roman.Value)
            {
                sb.Append(roman.Digit);
                value -= roman.Value;
            }
        }

        return sb.ToString();
    }
}