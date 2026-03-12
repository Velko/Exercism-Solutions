using System;

public static class ResistorColorDuo
{
    static readonly string[] BAND_COLORS = new[] { "black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "grey", "white"};
    
    public static int Value(string[] colors)
        => Array.IndexOf(BAND_COLORS, colors[0]) * 10 
         + Array.IndexOf(BAND_COLORS, colors[1]);
}
