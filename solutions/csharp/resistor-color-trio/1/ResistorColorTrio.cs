using System;

public static class ResistorColorTrio
{
    static readonly string[] BAND_COLORS = new[] { "black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "grey", "white"};
    
    public static string Label(string[] colors)
    {
        var base_value = Array.IndexOf(BAND_COLORS, colors[0]) * 10 
                       + Array.IndexOf(BAND_COLORS, colors[1]);
        var multiplier = Array.IndexOf(BAND_COLORS, colors[2]);

        if (colors[1] == "black")
        {
            base_value /= 10;
            ++multiplier;
        }
                    
        base_value = (multiplier % 3) switch
        {
            1 => base_value * 10,
            2 => base_value * 100,
            _ => base_value,
        };

        var prefix = (multiplier / 3) switch
        {
            1 => "kilo",
            2 => "mega",
            _ => ""
        };
        
        return $"{base_value} {prefix}ohms";
    }
}
