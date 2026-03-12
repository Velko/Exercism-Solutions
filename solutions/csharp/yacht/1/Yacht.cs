using System;
using System.Linq;

public enum YachtCategory
{
    Ones = 1,
    Twos = 2,
    Threes = 3,
    Fours = 4,
    Fives = 5,
    Sixes = 6,
    FullHouse = 7,
    FourOfAKind = 8,
    LittleStraight = 9,
    BigStraight = 10,
    Choice = 11,
    Yacht = 12,
}

public static class YachtGame
{
    public static int Score(int[] dice, YachtCategory category)
        => category switch {
            YachtCategory.Yacht when IsYacht(dice) => 50,
            YachtCategory.Ones => CountSingle(dice, 1),
            YachtCategory.Twos => CountSingle(dice, 2),
            YachtCategory.Threes => CountSingle(dice, 3),
            YachtCategory.Fours => CountSingle(dice, 4),
            YachtCategory.Fives => CountSingle(dice, 5),
            YachtCategory.Sixes => CountSingle(dice, 6),
            YachtCategory.FullHouse => CountFullHouse(dice),
            YachtCategory.FourOfAKind => Count4OrMore(dice),
            YachtCategory.LittleStraight when IsLittleStraight(dice) => 30,
            YachtCategory.BigStraight when IsBigStraight(dice) => 30,
            YachtCategory.Choice => dice.Sum(),
            _ => 0,
        };

    private static bool IsYacht(int[] dice)
        => dice.Distinct().Count() == 1;

    private static int CountSingle(int[] dice, int value)
        => dice.Where(d => d == value).Sum();

    private static int CountFullHouse(int[] dice)
    {
        var grouped = dice
            .GroupBy(g => g)
            .Select(g => g.Count() )
            .OrderDescending()
            .ToList();

        if (grouped.SequenceEqual(new[] {3, 2}))
            return dice.Sum();

        return 0;
    }

    private static int Count4OrMore(int[] dice)
    {
        var selected = dice
            .GroupBy(g => g)
            .Select(g => new { count = g.Count(), score = g.First() * 4 } )
            .SingleOrDefault(s => s.count >= 4);

        return selected?.score ?? 0;
    }

    private static bool IsLittleStraight(int[] dice)
        =>
            dice
                .Order()
                .SequenceEqual(new[] {1, 2, 3, 4, 5});

    private static bool IsBigStraight(int[] dice)
        =>
            dice
                .Order()
                .SequenceEqual(new[] {2, 3, 4, 5, 6});
}

