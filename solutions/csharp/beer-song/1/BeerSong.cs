using System;
using System.Collections.Generic;

public static class BeerSong
{
    public static string Recite(int startBottles, int takeDown)
    {
        var verses = new List<string>();

        for (int bottles = startBottles; bottles > startBottles-takeDown; --bottles)
            verses.Add(Verse(bottles));

        return string.Join("\n\n", verses);
    }

    static string Verse(int n) => Capitalize($"{FormatWall(n)}, {BottlesOfBeer(n)}.\n{FormatRemaining(n, 99)}");
    static string FormatWall(int n) => $"{BottlesOfBeer(n)} on the wall";
    static string BottlesOfBeer(int n) => $"{FormatNumber(n)} {Bottles(n)} of beer";
    static string FormatNumber(int n) => n == 0 ? "no more" : n.ToString();
    static string Bottles(int n) => $"bottle{(n == 1 ? "" : "s")}";
    static string FormatRemaining(int n, int reset)
        => n == 0
        ? $"Go to the store and buy some more, {FormatWall(reset)}."
        : $"Take {FormatSingle(n)} down and pass it around, {FormatWall(n - 1)}.";
    static string FormatSingle(int n) => n == 1 ? "it" : "one";
    static string Capitalize(string s) => s[0..1].ToUpper() + s[1..];
}