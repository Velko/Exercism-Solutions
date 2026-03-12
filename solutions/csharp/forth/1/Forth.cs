using System;
using System.Collections.Generic;
using System.Linq;

using Sprache;

public static class Forth
{
    private static readonly Parser<ParsedToken> SignedNumber =
        from minus in Parse.String("-").Optional()
        from num in Parse.Number
        select new ParsedToken
        {
            Type = TokenType.Number,
            Number = int.Parse((minus.IsDefined ? "-" : "") + num),
        };

    private static readonly Parser<string> Identifier =
        Parse.Identifier(Parse.Letter, Parse.LetterOrDigit.Or(Parse.Char('-')));

    private static readonly Parser<string> Operator =
        from c in Parse.Chars('+', '-', '*', '/')
        select c.ToString();

    private static readonly Parser<ParsedToken> NamedOperation =
        from c in Identifier.Or(Operator)
        select new ParsedToken
        {
            Type = TokenType.Name,
            Name = c,
        };

    private static readonly Parser<ParsedToken> Statement =
            SignedNumber
            .Or(NamedOperation);

    private static readonly Parser<ParsedToken> CustomDefinition =
            from _colon in Parse.Char(':')
            from _leading in Parse.WhiteSpace.Many()
            from name in Identifier.Or(Operator)
            from _separator in Parse.WhiteSpace.AtLeastOnce()
            from statements in Statement.DelimitedBy(Parse.WhiteSpace)
            from _trailing in Parse.WhiteSpace.Many()
            from _semi in Parse.Char(';')
            select new ParsedToken
            {
                Type = TokenType.Definition,
                Name = name,
                Steps = statements.ToArray(),
            };

    public static readonly Parser<IEnumerable<ParsedToken>> Grammar =
        from statement in Statement.Or(CustomDefinition).DelimitedBy(Parse.WhiteSpace)
        select statement;

    public static string Evaluate(string[] instructions)
    {
        var eval = new ForthEvaluator();

        List<int> result = eval.Evaluate(instructions);

        result.Reverse();
        return string.Join(" ", result);
    }
}

public enum TokenType
{
    Number,
    Name,
    Definition,
}

public class ParsedToken
{
    public TokenType Type { get; init; }
    public int Number { get; init; }
    public string Name { get; init; }
    public ParsedToken[] Steps { get; init; }
}

class ForthEvaluator
{
    private Dictionary<string, Action[]> operation_defs;
    private Stack<int> stack;

    public ForthEvaluator()
    {
        stack = new Stack<int>();

        operation_defs = new Dictionary<string, Action[]>
        {
            {"+", new [] { AddNumbers }},
            {"-", new [] { SubNumbers }},
            {"*", new [] { MulNumbers }},
            {"/", new [] { DivNumbers }},
            {"DUP", new [] { DupNumber }},
            {"DROP", new [] { DropNumber }},
            {"SWAP", new [] { SwapNumbers }},
            {"OVER", new [] { OverNumber }},
        };
    }

    IEnumerable<ParsedToken> ParseInstruction (string instruction)
    {
        try
        {
            return Forth.Grammar.Parse(instruction);
        }
        catch (Sprache.ParseException e)
        {
            throw new InvalidOperationException(e.Message, e);
        }
    }

    public List<int> Evaluate(string[] instructions)
    {
        foreach (string instruction in instructions)
        {
            var tokens = ParseInstruction(instruction);
            foreach (var token in tokens)
            {
                ResolveOperation(token)();
            }
        }

        return stack.ToList();
    }

    Action ResolveOperation(ParsedToken token)
    {
        switch (token.Type)
        {
            case TokenType.Number:
                return () => stack.Push(token.Number);
            case TokenType.Name:
                if (!operation_defs.TryGetValue(token.Name.ToUpper(), out var ops))
                    throw new InvalidOperationException();
                return () => {
                    foreach (var op in ops)
                        op();
                };
            case TokenType.Definition:
                operation_defs[token.Name.ToUpper()] = token.Steps.Select(ResolveOperation).ToArray();
                return () => {};
            default:
                throw new NotImplementedException();
        }
    }


    private void DupNumber()
    {
        var val = stack.Peek();
        stack.Push(val);
    }

    private void DropNumber()
    {
        _ = stack.Pop();
    }

    private void SwapNumbers()
    {
        var arg1 = stack.Pop();
        var arg0 = stack.Pop();

        stack.Push(arg1);
        stack.Push(arg0);
    }

    private void OverNumber()
    {
        var val = stack.Skip(1).First();
        stack.Push(val);
    }

    private void AddNumbers()
    {
        var arg1 = stack.Pop();
        var arg0 = stack.Pop();

        stack.Push(arg0 + arg1);
    }

    private void SubNumbers()
    {
        var arg1 = stack.Pop();
        var arg0 = stack.Pop();

        stack.Push(arg0 - arg1);
    }

    private void MulNumbers()
    {
        var arg1 = stack.Pop();
        var arg0 = stack.Pop();

        stack.Push(arg0 * arg1);
    }

    private void DivNumbers()
    {
        var arg1 = stack.Pop();
        var arg0 = stack.Pop();

        stack.Push(arg0 / arg1);
    }
}
