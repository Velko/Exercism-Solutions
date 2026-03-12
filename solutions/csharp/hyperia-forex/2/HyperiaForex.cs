using System;
using System.Diagnostics.CodeAnalysis;

public struct CurrencyAmount
{
    private decimal amount;
    private string currency;

    public CurrencyAmount(decimal amount, string currency)
    {
        this.amount = amount;
        this.currency = currency;
    }

    public override bool Equals(object obj) => obj is CurrencyAmount other && this == other;
    public override int GetHashCode() => HashCode.Combine(amount, currency);

    public static bool operator == (CurrencyAmount a, CurrencyAmount b)
    {
        if (a.currency != b.currency)
            throw new ArgumentException();

        return a.amount == b.amount;
    }

    public static bool operator != (CurrencyAmount a, CurrencyAmount b)
    {
        if (a.currency != b.currency)
            throw new ArgumentException();

        return a.amount != b.amount;
    }

    public static bool operator < (CurrencyAmount a, CurrencyAmount b) {
        if (a.currency != b.currency)
            throw new ArgumentException();

        return a.amount < b.amount;
    }

    public static bool operator > (CurrencyAmount a, CurrencyAmount b) {
        if (a.currency != b.currency)
            throw new ArgumentException();

        return a.amount > b.amount;
    }

    public static CurrencyAmount operator + (CurrencyAmount a, CurrencyAmount b) {
        if (a.currency != b.currency)
            throw new ArgumentException();

        return new CurrencyAmount(a.amount + b.amount, a.currency);
    }

    public static CurrencyAmount operator - (CurrencyAmount a, CurrencyAmount b) {
        if (a.currency != b.currency)
            throw new ArgumentException();

        return new CurrencyAmount(a.amount - b.amount, a.currency);
    }

    public static CurrencyAmount operator * (CurrencyAmount a, decimal multiplier) {
        return new CurrencyAmount(a.amount * multiplier, a.currency);
    }

    public static CurrencyAmount operator * (decimal multiplier, CurrencyAmount a) {
        return new CurrencyAmount(a.amount * multiplier, a.currency);
    }

    public static CurrencyAmount operator / (CurrencyAmount a, decimal divider) {
        return new CurrencyAmount(a.amount / divider, a.currency);
    }

    public static implicit operator decimal(CurrencyAmount a)
        => a.amount;

    public static explicit operator double(CurrencyAmount a)
        => (double)a.amount;
}
