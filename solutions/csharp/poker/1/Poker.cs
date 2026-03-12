using System;
using System.Collections.Generic;
using System.Linq;

enum Suit
{
    Clubs,
    Hearts,
    Diamonds,
    Spades,
}

struct Rank
{
    public int Value { get; init; }

    public static readonly Rank A = new Rank { Value = 14 };
    public static readonly Rank K = new Rank { Value = 13 };
    public static readonly Rank Q = new Rank { Value = 12 };
    public static readonly Rank J = new Rank { Value = 11 };

    // numeric cards 2 .. 10

    // Ace for low-straight checks
    public static readonly Rank[] A_L = new[] { new Rank { Value = 1 } };
}

struct Card
{
    public Suit Suit { get; init; }
    public Rank Rank { get; init; }
}

enum Combination
{
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    RoyalFlush,
}

class Hand : IComparable<Hand>
{
    public Combination Combination { get; init; }
    public Rank[] SignificantCards { get; init; }
    public string Original { get; init; }

    public int CompareTo(Hand other)
    {
        if (Combination != other.Combination)
            return other.Combination - Combination;

        foreach (var (a, b) in SignificantCards.Zip(other.SignificantCards))
        {
            if (a.Value != b.Value)
                return b.Value - a.Value;
        }

        return 0;
    }
}

public static class Poker
{
    public static IEnumerable<string> BestHands(IEnumerable<string> hands)
    {
        var parsed_hands = hands.Select(ParseHand).Order().ToList();

        var best = parsed_hands.First();

        return parsed_hands
            .TakeWhile(h => h.CompareTo(best) == 0)
            .Select(o => o.Original);
    }

    static Suit ParseSuit(char s)
        => s switch {
            'C' => Suit.Clubs,
            'H' => Suit.Hearts,
            'D' => Suit.Diamonds,
            'S' => Suit.Spades,
            _ => throw new ArgumentOutOfRangeException()
        };

    static Rank ParseRank(string s)
        => s switch {
            "A" => Rank.A,
            "K" => Rank.K,
            "Q" => Rank.Q,
            "J" => Rank.J,
            _ when int.TryParse(s, out var val) && val is >= 1 and <= 10 => new Rank { Value = val },
            _ => throw new ArgumentOutOfRangeException(),
        };

    static Card ParseCard(string s)
        => new Card
        {
            Suit = ParseSuit(s[^1]),
            Rank = ParseRank(s[..^1]),
        };

    static Hand ParseHand(string hand_str)
    {
        var hand = hand_str
                    .Split()
                    .Select(ParseCard)
                    .ToArray();

        var is_flush = hand
                        .DistinctBy(s => s.Suit)
                        .Count() == 1;

        var ranks = hand
                    .Select(r => r.Rank)
                    .OrderByDescending(o => o.Value)
                    .ToArray();

        var straight = ranks.CheckStraight();

        var grouped = ranks.GroupByCount();

        var (combination, significant) =
            TryRoyalFlush(straight, is_flush) ??
            TryStraightFlush(straight, is_flush) ??
            TryNOfAKind(grouped, 4, Combination.FourOfAKind) ??
            TryNOfAKind(grouped, 3, 2, Combination.FullHouse) ??
            TryFlush(ranks, is_flush) ??
            TryStraight(straight) ??
            TryNOfAKind(grouped, 3, Combination.ThreeOfAKind) ??
            TryNOfAKind(grouped, 2, 2, Combination.TwoPair) ??
            TryNOfAKind(grouped, 2, Combination.Pair) ??
            (Combination.HighCard, ranks);

        return new Hand
        {
            Combination = combination,
            SignificantCards = significant,
            Original = hand_str,
        };
    }

    static Rank[] CheckStraight(this Rank[] ranks)
    {
        if (ranks.AreConsecutive())
            return ranks.Take(1).ToArray();

        if (ranks.First().Value == Rank.A.Value)
        {
            return ranks
                    .Skip(1)
                    .Concat(Rank.A_L)
                    .ToArray()
                    .CheckStraight();
        }

        return new Rank[0];
    }

    static bool AreConsecutive(this Rank[] ranks)
        => ranks
            .Zip(ranks.Skip(1))
            .All(r => r.First.Value - r.Second.Value == 1);

    static (int count, Rank rank)[] GroupByCount(this Rank[] ranks)
        => ranks
            .GroupBy(k => k.Value)
            .Select(g => ( g.Count(), g.First()))
            .OrderByDescending(o => o.Item1)
            .ToArray();

    static (Combination,Rank[])? TryRoyalFlush(Rank[] straight, bool is_flush)
        => is_flush && straight.Any() && straight[0].Value == Rank.A.Value
        ? (Combination.RoyalFlush, straight)
        : null;

    static (Combination,Rank[])? TryStraightFlush(Rank[] straight, bool is_flush)
        => is_flush && straight.Any()
        ? (Combination.StraightFlush, straight)
        : null;

    static (Combination,Rank[])? TryNOfAKind((int count, Rank rank)[] grouped, int group_size, Combination target)
        => TryNOfAKind(grouped, group_size, 1, target);

    static (Combination,Rank[])? TryNOfAKind((int count, Rank rank)[] grouped, int group1_size, int group2_size, Combination target)
        => grouped is [(var x, _), (var y, _), ..]
            && x == group1_size && y == group2_size
        ? (target, grouped.Select(a => a.rank).ToArray())
        : null;

    static (Combination,Rank[])? TryFlush(Rank[] ranks, bool is_flush)
        => is_flush
        ? (Combination.Flush, ranks)
        : null;

    static (Combination,Rank[])? TryStraight(Rank[] straight)
        => straight.Any()
        ? (Combination.Straight, straight)
        : null;
}