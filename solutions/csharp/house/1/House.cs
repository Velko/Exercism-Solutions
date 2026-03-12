using System;
using System.Text;
using System.Collections.Generic;

public static class House
{
    static readonly (string subject, string action)[] Verses = new[]
    {
        ("horse and the hound and the horn", "belonged to"),
        ("farmer sowing his corn", "kept"),
        ("rooster that crowed in the morn", "woke"),
        ("priest all shaven and shorn", "married"),
        ("man all tattered and torn", "kissed"),
        ("maiden all forlorn", "milked"),
        ("cow with the crumpled horn", "tossed"),
        ("dog", "worried"),
        ("cat", "killed"),
        ("rat", "ate"),
        ("malt", "lay in"),
        ("house that Jack built.", null),
    };

    public static string Recite(int verseNumber)
    {
        var sb = new StringBuilder("This is the ");
        var action = string.Empty;

        for (var i = Verses.Length - verseNumber; i < Verses.Length; ++i)
        {
            sb.Append(action);
            sb.Append(Verses[i].subject);
            action = $" that {Verses[i].action} the ";
        }

        return sb.ToString();
    }

    public static string Recite(int startVerse, int endVerse)
    {
        var verses = new List<string>();

        for (var verse = startVerse; verse <= endVerse; ++verse)
            verses.Add(Recite(verse));

        return string.Join("\n", verses);
    }
}