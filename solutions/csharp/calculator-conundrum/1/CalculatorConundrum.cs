using System;

public static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string operation)
        => operation switch
        {
            "+" => $"{operand1} {operation} {operand2} = {operand1 + operand2}",
            "*" => $"{operand1} {operation} {operand2} = {operand1 * operand2}",
            "/" when operand2 == 0 => $"Division by zero is not allowed.",
            "/" => $"{operand1} {operation} {operand2} = {operand1 / operand2}",
            "" => throw new ArgumentException(nameof(operation)),
            null => throw new ArgumentNullException(nameof(operation)),
            _ => throw new ArgumentOutOfRangeException(),
        };
}
