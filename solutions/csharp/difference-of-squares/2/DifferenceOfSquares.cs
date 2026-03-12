/* Intentionally low-level implementation to understand the algorythm better
   for other languages that does not have functional facilities.
 */
public static class DifferenceOfSquares
{
    public static int CalculateSquareOfSum(int max)
    {
        int result = 0;
        for (int n = 1; n <= max; ++n)
            result += n;

        return result * result;
    }

    public static int CalculateSumOfSquares(int max)
    {
        int result = 0;
        for (int n = 1; n <= max; ++n)
            result += n * n;

        return result;
    }


// square_of_sum is actually sum_of_squares + "something". That "something" is exactly what we need to calculate

// sum of 2 numbers squared
// (a+b) * (a+b) => a*a + a*b + b*a + b*b = a*a + 2*a*b + b*b

// sum of 3 numbers squared
// (a+b+c) * (a+b+c)  => (a*a + a*b + a*c) + (a*b + b*b + b*c) + (a*c + b*c + c*c) => (a*a + b*b + c*c ) + (2*a*b + 2*a*c + 2*b*c)

// sum of 4 numbers squared
// (a+b+c+d)*(a+b+c+d) => (a*a + a*b + a*c + a*d) + (b*a + b*b + b*c + b*d) + (c*a + c*b + c*c + c*d) + (d*a + d*b + d*c + d*d)
//                     => (a*a + a*b + a*c + a*d) + (a*b + b*b + b*c + b*d) + (a*c + b*c + c*c + c*d) + (a*d + b*d + c*d + d*d)
//                     => (2*a*b + 2*a*c + 2*a*d + 2*b*c + 2*b*d + 2*c*d) + (a*a + b*b + c*c + d*d)
//                     => 2 * (a*b + a*c + a*d + b*c + b*d + c*d) + (a*a + b*b + c*c + d*d)
//                        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ - the part we need
    public static int CalculateDifferenceOfSquares(int max)
    {
        int result = 0;

        for (int j = 1; j <= max; ++j)
        {
            for (int i = j + 1; i <= max; ++i)
                result += i * j;
        }

        return result * 2;
    }
}