using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BinarySearchTree : IEnumerable<int>
{
    public BinarySearchTree(int value)
    {
        Value = value;
    }

    public BinarySearchTree(IEnumerable<int> values)
        : this(values.First())
    {
        var subValues = values.Skip(1);
        foreach (var value in subValues)
            _ = Add(value);
    }

    public int Value { get; }

    public BinarySearchTree Left { get; private set; }

    public BinarySearchTree Right { get; private set; }

    public BinarySearchTree Add(int value)
    {
        if (value <= Value)
        {
            // Add to existing or create new
            Left = Left?.Add(value) ?? new BinarySearchTree(value);
        }
        else
        {
            Right = Right?.Add(value) ?? new BinarySearchTree(value);
        }

        return this;
    }

    public IEnumerator<int> GetEnumerator()
        => Walk().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    private IEnumerable<int> Walk()
    {
        // instead of fighting the intricacies of IEnumerator<>, simply forward it to
        // a *generator* method.

        // The algorithm then becomes incredibly simple:
        //  * all nodes from the Left
        //  * itself
        //  * all nodes from the Right

        if (Left != null)
            foreach (var smaller in Left.Walk())
                yield return smaller;

        yield return Value;

        if (Right != null)
            foreach (var larger in Right.Walk())
                yield return larger;
    }
}