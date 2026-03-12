using System;

public class Queen
{
    public Queen(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row { get; }
    public int Column { get; }
}

public static class QueenAttack
{
    public static bool CanAttack(Queen white, Queen black)
        => white.Row == black.Row
        || white.Column == black.Column
        || Math.Abs(white.Column - black.Column) == Math.Abs(white.Row - black.Row);

    public static Queen Create(int row, int column)
    {
        if (row is >=0 and < 8 && column is >=0 and < 8)
            return new Queen(row, column);

        throw new ArgumentOutOfRangeException();
    }
}