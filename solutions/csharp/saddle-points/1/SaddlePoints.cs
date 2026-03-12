using System;
using System.Collections.Generic;
using System.Linq;

public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        var max_cells = new HashSet<(int, int)>(MaxPerRows(matrix));
        var min_cells = new HashSet<(int, int)>(MinPerCols(matrix));

        return max_cells.Intersect(min_cells).ToArray();
    }

    private static IEnumerable<(int, int)> MaxPerRows(int[,] matrix)
    {
        var height = matrix.GetLength(0);
        var width = matrix.GetLength(1);

        for (int row = 0; row < height; ++row)
        {
            var max_val = int.MinValue;

            for (int col = 0; col < width; ++col)
                if (max_val < matrix[row, col])
                    max_val = matrix[row, col];

            for (int col = 0; col < width; ++col)
                if (max_val == matrix[row, col])
                    yield return (row + 1, col + 1);
        }
    }

    private static IEnumerable<(int, int)> MinPerCols(int[,] matrix)
    {
        var height = matrix.GetLength(0);
        var width = matrix.GetLength(1);

        for (int col = 0; col < width; ++col)
        {
            var min_val = int.MaxValue;

            for (int row = 0; row < height; ++row)
                if (min_val > matrix[row, col])
                    min_val = matrix[row, col];

            for (int row = 0; row < height; ++row)
                if (min_val == matrix[row, col])
                    yield return (row + 1, col + 1);
        }
    }
}
