using System;
using System.Collections.Generic;
using System.Linq;

public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r)
        => r.Expreal(realNumber);
}

public struct RationalNumber
{
    private readonly int numerator;
    private readonly int denominator;
    public RationalNumber(int numerator, int denominator)
    {
        HashSet<int> Factor(int number)
            => Enumerable.Range(
                    1,
                    Math.Abs(number) + 1)
                .Where(i => number % i == 0)
                .ToHashSet();

        int gcf = denominator;

        if (numerator != 0)
        {
            var numFactors = Factor(numerator);
            var denFactors = Factor(denominator);
            gcf = numFactors.Intersect(denFactors).Max();

            if (denominator < 0)
                gcf = -gcf;
        }

        this.numerator = numerator / gcf;
        this.denominator = denominator / gcf;
    }

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
        => new RationalNumber(
                r1.numerator * r2.denominator + r2.numerator * r1.denominator,
                r1.denominator * r2.denominator);

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
        => new RationalNumber(
                r1.numerator * r2.denominator - r2.numerator * r1.denominator,
                r1.denominator * r2.denominator);

    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
        => new RationalNumber(
                r1.numerator * r2.numerator,
                r1.denominator * r2.denominator);

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
        => new RationalNumber(
                r1.numerator * r2.denominator,
                r1.denominator * r2.numerator);

    public RationalNumber Abs()
        => new RationalNumber(
            Math.Abs(numerator),
            Math.Abs(denominator));

    public RationalNumber Reduce()
        => this;

    public RationalNumber Exprational(int power)
        => power switch {
            > 0 => Enumerable.Repeat(this, power).Aggregate((a, b) => a * b),
            0 => new RationalNumber(1, 1),
            < 0 => Enumerable.Repeat(new RationalNumber(denominator, numerator), -power).Aggregate((a, b) => a * b),
        };

    public double Expreal(int baseNumber)
        => Math.Pow(baseNumber, (double)numerator / denominator);
}