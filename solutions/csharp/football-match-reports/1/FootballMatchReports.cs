using System;

public static class PlayAnalyzer
{
    public static string AnalyzeOnField(int shirtNum)
        => shirtNum switch {
            1 => "goalie",
            2 => "left back",
            3 or 4 => "center back",
            5 => "right back",
            6 or 7 or 8 => "midfielder",
            9 => "left wing",
            10 => "striker",
            11 => "right wing",
            _ => throw new ArgumentOutOfRangeException(),
        };

    public static string AnalyzeOffField(object report)
    {
        if (report is int)
            return $"There are {report} supporters at the match.";

        if (report is string reportStr)
            return reportStr;

        if (report is Incident incident)
        {
            if (incident is Injury)
                return $"Oh no! {incident.GetDescription()} Medics are on the field.";
    
            return incident.GetDescription();
        }

        if (report is Manager manager)
        {
            if (manager.Club != null)
                return $"{manager.Name} ({manager.Club})";
            else
                return manager.Name;
        }

        throw new ArgumentException(nameof(report));
    }
}
