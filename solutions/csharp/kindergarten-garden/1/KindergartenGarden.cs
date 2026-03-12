using System;
using System.Collections.Generic;

public enum Plant
{
    Violets,
    Radishes,
    Clover,
    Grass
}

public class KindergartenGarden
{
    string[] plants;
    
    public KindergartenGarden(string diagram)
    {
        plants = diagram.Split('\n');
    }

    public IEnumerable<Plant> Plants(string student)
    {
        var studentIndex = student[0] - 'A';

        var studentPlants = plants[0].Substring(studentIndex * 2, 2) + 
                            plants[1].Substring(studentIndex * 2, 2);

        foreach (var plant in studentPlants)
            yield return plant switch
            {
                'V' => Plant.Violets,
                'R' => Plant.Radishes,
                'C' => Plant.Clover,
                'G' => Plant.Grass,
                _ => throw new ArgumentException(),
            };
    }
}