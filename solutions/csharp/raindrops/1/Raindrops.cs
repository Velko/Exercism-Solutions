using System;
using System.Collections.Generic;

public static class Raindrops
{
    struct FactorSound
    {
        public int Factor;
        public string Sound;
    }

    static FactorSound[] Sounds = new []
    {
        new FactorSound { Factor = 3, Sound = "Pling" },
        new FactorSound { Factor = 5, Sound = "Plang" },
        new FactorSound { Factor = 7, Sound = "Plong" },
    };

    public static string Convert(int number)
    {
        var sounds = new List<string>();

        foreach (var factor in Sounds)
        {
            if (number % factor.Factor == 0)
                sounds.Add(factor.Sound);
        }

        return sounds.Count > 0
            ? string.Join(string.Empty, sounds)
            : number.ToString();
    }
}