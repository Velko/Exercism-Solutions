using System.Collections.Generic;

public static class BottleSong
{
    static readonly string[] numbers = {
        "No",
        "One",
        "Two",
        "Three",
        "Four",
        "Five",
        "Six",
        "Seven",
        "Eight",
        "Nine",
        "Ten"
    };
    
    public static IEnumerable<string> Recite(int startBottles, int takeDown)
    {
        for (int n = 0; n < takeDown; ++n,--startBottles)
        {
            if (n > 0) yield return "";
            yield return $"{numbers[startBottles]} green {SingularOrPluralBottle(startBottles)} hanging on the wall,";
            yield return $"{numbers[startBottles]} green {SingularOrPluralBottle(startBottles)} hanging on the wall,";
            yield return $"And if one green bottle should accidentally fall,";
            yield return $"There'll be {numbers[startBottles-1].ToLower()} green {SingularOrPluralBottle(startBottles-1)} hanging on the wall.";
        }
    }

    private static string SingularOrPluralBottle(int num) =>
        num == 1 ? "bottle" : "bottles";
}
