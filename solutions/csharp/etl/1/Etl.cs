using System;
using System.Collections.Generic;
using System.Linq;

public static class Etl
{
    public static Dictionary<string, int> Transform(Dictionary<int, string[]> old)
        => new Dictionary<string, int> (
            old.SelectMany(nc => nc.Value, (score, chr) => new KeyValuePair<string, int>(chr.ToLower(), score.Key))
        );
}