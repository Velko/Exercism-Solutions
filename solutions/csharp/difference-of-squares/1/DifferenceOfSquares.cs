using System;

public static class DifferenceOfSquares
{
    public static int CalculateSquareOfSum(int max)
        => (max*(max+1)/2)*(max*(max+1)/2);

    public static int CalculateSumOfSquares(int max)
        => max*(max+1)*(2*max+1)/6;

    public static int CalculateDifferenceOfSquares(int max)
        => max*(max+1)*(3*max*max-max-2)/12;
}