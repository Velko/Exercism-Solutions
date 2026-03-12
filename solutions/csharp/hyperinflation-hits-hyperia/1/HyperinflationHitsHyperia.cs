using System;

public static class CentralBank
{
    public static string DisplayDenomination(long @base, long multiplier)
    {
        try
        {
            checked {
                return (@base * multiplier).ToString();
            }
        }
        catch (OverflowException)
        {
            return "*** Too Big ***";
        }
    }

    public static string DisplayGDP(float @base, float multiplier)
    {
        var gdpVal = (@base * multiplier);

        return gdpVal < float.PositiveInfinity ? gdpVal.ToString() : "*** Too Big ***";
    }

    public static string DisplayChiefEconomistSalary(decimal salaryBase, decimal multiplier)
    {
        try
        {
            checked {
                return (salaryBase * multiplier).ToString();
            }
        }
        catch (OverflowException)
        {
            return "*** Much Too Big ***";
        }
    }
}
