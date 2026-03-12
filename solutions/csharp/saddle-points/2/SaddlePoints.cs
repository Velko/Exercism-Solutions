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

        for (int row = 0; row < height; ++row)
        {
            var max_val = matrix
                .GetRow(row)
                .Max(v => v.value);

            var max_cells = matrix
                .GetRow(row)
                .Where(v => v.value == max_val)
                .Select(c => ( row + 1, c.index + 1));

            foreach (var cell in max_cells)
                yield return cell;
        }
    }

    private static IEnumerable<(int, int)> MinPerCols(int[,] matrix)
    {
        var width = matrix.GetLength(1);

        for (int col = 0; col < width; ++col)
        {
            var min_val = matrix
                .GetColumn(col)
                .Min(v => v.value);

            var min_cells = matrix
                .GetColumn(col)
                .Where(v => v.value == min_val)
                .Select(c => (c.index + 1, col + 1));

            foreach (var cell in min_cells)
                yield return cell;
        }
    }

    private static IEnumerable<(int index, int value)> GetRow(this int[,] matrix, int row)
    {
        var width = matrix.GetLength(1);

        for (int col = 0; col < width; ++col)
            yield return (col, matrix[row, col]);
    }

    private static IEnumerable<(int index, int value)> GetColumn(this int[,] matrix, int col)
    {
        var height = matrix.GetLength(0);

        for (int row = 0; row < height; ++row)
            yield return (row, matrix[row, col]);
    }
}
