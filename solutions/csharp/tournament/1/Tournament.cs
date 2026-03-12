using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public static class Tournament
{
    public static void Tally(Stream inStream, Stream outStream)
    {
        using var reader = new StreamReader(inStream, Encoding.UTF8);
        using var writer = new StreamWriter(outStream, new UTF8Encoding(false));

        writer.Write("Team                           | MP |  W |  D |  L |  P");

        var team_stats = new List<TeamStats>();

        while (!reader.EndOfStream)
        {
            var line_parts = reader.ReadLine().Split(';');

            switch (line_parts[2])
            {
                case "win":
                    team_stats.Add(new TeamStats { Team = line_parts[0], Wins = 1 });
                    team_stats.Add(new TeamStats { Team = line_parts[1], Losses = 1 });
                    break;
                case "loss":
                    team_stats.Add(new TeamStats { Team = line_parts[0], Losses = 1 });
                    team_stats.Add(new TeamStats { Team = line_parts[1], Wins = 1 });
                    break;
                case "draw":
                    team_stats.Add(new TeamStats { Team = line_parts[0], Ties = 1 });
                    team_stats.Add(new TeamStats { Team = line_parts[1], Ties = 1 });
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        var report =
            from stats in team_stats
            group stats by stats.Team into team
            select new TeamStats
            {
                Team = team.Key,
                Played = team.Count(),
                Wins = team.Sum(s => s.Wins),
                Ties = team.Sum(s => s.Ties),
                Losses = team.Sum(s => s.Losses),
                Points = team.Sum(s => s.Wins * 3 + s.Ties),
            } into summary
            orderby summary.Points descending, summary.Team ascending
            select summary;

        foreach (var item in report)
        {
            writer.Write($"\n{item.Team,-31}| {item.Played,2} | {item.Wins,2} | {item.Ties,2} | {item.Losses,2} | {item.Points,2}");
        }
    }
}

struct TeamStats
{
    public string Team;
    public int? Played;
    public int Wins;
    public int Ties;
    public int Losses;
    public int? Points;
}
