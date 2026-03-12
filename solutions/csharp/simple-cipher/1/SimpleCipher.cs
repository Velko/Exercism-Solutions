using System;
using System.Linq;
using System.Collections.Generic;

public class SimpleCipher
{
    int[] _key;

    const int LETTERS_IN_ALPHABET = 'z' - 'a' + 1;

    public SimpleCipher()
    {
        var rand = new Random();
        _key = Enumerable.Range(0, 100)
            .Select(_ => rand.Next(26))
            .ToArray();
    }

    public SimpleCipher(string key)
    {
        _key = key
                .UnpackLetters()
                .ToArray();
    }

    public string Key => _key.PackLetters();

    public string Encode(string plaintext)
        => plaintext
                .UnpackLetters()
                .Zip(_key.RepeatForever())
                .Select(ck => (ck.First + ck.Second) % LETTERS_IN_ALPHABET)
                .PackLetters();

    public string Decode(string ciphertext)
        => ciphertext
                .UnpackLetters()
                .Zip(_key.RepeatForever())
                .Select(ck => (LETTERS_IN_ALPHABET + ck.First - ck.Second) % LETTERS_IN_ALPHABET)
                .PackLetters();
}

static class Extensions
{
    public static IEnumerable<T> RepeatForever<T>(this IEnumerable<T> collection)
    {
        for (;;)
        {
            foreach (var item in collection)
                yield return item;
        }
    }

    public static string PackLetters(this IEnumerable<int> letters)
        => new string(
            letters.Select(c => (char)(c + 'a'))
                .ToArray()
        );

    public static IEnumerable<int> UnpackLetters(this string text)
        => text.Select(c => c - 'a');
}