using System;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class RobotSimulator
{
    public RobotSimulator(Direction direction, int x, int y)
    {
        Direction = direction;
        X = x;
        Y = y;
    }

    public Direction Direction { get; private set; }

    public int X { get; private set; }

    public int Y { get; private set; }

    public void Move(string instructions)
    {
        foreach (var cmd in instructions)
        {
            switch (cmd)
            {
                case 'A':
                    Advance();
                    break;
                case 'L':
                    TurnLeft();
                    break;
                case 'R':
                    TurnRight();
                    break;
            }
        }
    }

    void Advance()
    {
        switch (Direction)
        {
            case Direction.North:
                ++Y;
                break;
            case Direction.East:
                ++X;
                break;
            case Direction.South:
                --Y;
                break;
            case Direction.West:
                --X;
                break;
        }
    }

    void TurnLeft()
    {
        Direction = Direction switch
        {
            Direction.North => Direction.West,
            Direction.West => Direction.South,
            Direction.South => Direction.East,
            Direction.East => Direction.North,
            _ => throw new InvalidOperationException(),
        };
    }
        

    void TurnRight()
    {
        Direction = Direction switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => throw new InvalidOperationException(),
        };
    }
}