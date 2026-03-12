using System;
using System.Linq;

public class DndCharacter
{
    public int Strength { get; init; }
    public int Dexterity { get; init; }
    public int Constitution { get; init; }
    public int Intelligence { get; init; }
    public int Wisdom { get; init; }
    public int Charisma { get; init; }
    public int Hitpoints { get; init; }

    public static int Modifier(int score)
        => (int)Math.Floor((score - 10) / 2.0);

    static readonly Random rnd = new Random();
    
    public static int Ability() 
        => Enumerable.Repeat(0, 4)
            .Select(_ => rnd.Next(6) + 1)
            .OrderBy(n => n)
            .Skip(1)
            .Sum();

    public static DndCharacter Generate()
    {
        var constitution = Ability();
        
        return new DndCharacter
        {
            Strength = Ability(),
            Dexterity = Ability(),
            Constitution = constitution,
            Intelligence = Ability(),
            Wisdom = Ability(),
            Charisma = Ability(),
            Hitpoints = 10 + Modifier(constitution),
        };
    }
}
