using System;

public class SpiralMatrix
{
    public static int[,] GetMatrix(int size)
    {
        var matrix = new int[size, size];

        int l = 0, r = size - 1, t = 0, b = size - 1;

        for (int counter = 1; counter <= size * size; )
        {
            for (int x = l; x <= r; ++x)
                matrix[t, x] = counter++;

            ++t;

            for (int y = t ; y <= b; ++y)
                matrix[y, r] = counter++;

            --r;

            for (int x = r ; x >= l; --x)
                matrix[b, x] = counter++;

            --b;

            for (int y = b; y >= t; --y)
                matrix[y, l] = counter++;

            ++l;
        }

        return matrix;
    }
}
