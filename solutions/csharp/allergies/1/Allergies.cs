using System;
using System.Collections.Generic;

public enum Allergen
{
    Eggs = 1 << 0,
    Peanuts = 1 << 1,
    Shellfish = 1 << 2,
    Strawberries = 1 << 3,
    Tomatoes = 1 << 4,
    Chocolate = 1 << 5,
    Pollen = 1 << 6,
    Cats = 1 << 7
}

public class Allergies
{
    int allergies;

    public Allergies(int mask)
    {
        allergies = mask & 0xFF;
    }

    public bool IsAllergicTo(Allergen allergen) =>
        (allergies & (int)allergen) != 0;


    public Allergen[] List()
    {
        var allergens = new List<Allergen>();
        for (int a = (int)Allergen.Eggs; a <= (int)Allergen.Cats; a <<= 1)
        {
            if ((allergies & a) != 0)
                allergens.Add((Allergen)a);
        }

        return allergens.ToArray();
    }
}