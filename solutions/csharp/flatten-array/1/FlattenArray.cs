using System;
using System.Collections;

public static class FlattenArray
{
    public static IEnumerable Flatten(IEnumerable input)
    {
        foreach (var item in input)
        {
            if (item is IEnumerable enumerable)
            {
                foreach (var subitem in Flatten(enumerable))
                    yield return subitem;
            }
            else if (item != null)
            {
                yield return item;
            }
        }

    }
}