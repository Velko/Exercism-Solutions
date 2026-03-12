using System;
using System.Collections.Generic;

public struct Coord
{
    public Coord(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }

    public ushort X { get; }
    public ushort Y { get; }
}

public struct Plot
{
    Coord c0;
    Coord c1;
    Coord c2;
    Coord c3;

    public Plot(Coord c_0, Coord c_1, Coord c_2, Coord c_3)
    {
        c0 = c_0;
        c1 = c_1;
        c2 = c_2;
        c3 = c_3;
    }

    public (Coord, Coord)[] Sides => new[] {(c0, c1), (c1, c2), (c2, c3), (c3, c0)};
}


public class ClaimsHandler
{
    HashSet<Plot> plots = new HashSet<Plot>();
    Plot? lastClaim;

    public void StakeClaim(Plot plot)
    {
        plots.Add(plot);
        lastClaim = plot;
    }

    public bool IsClaimStaked(Plot plot)
        => plots.Contains(plot);

    public bool IsLastClaim(Plot plot)
        => plot.Equals(lastClaim);

    public Plot GetClaimWithLongestSide()
    {
        int longestSideSquared = 0;
        Plot? result = null;

        foreach (var plot in plots)
        {
            foreach (var (side_start, side_end) in plot.Sides)
            {
                /* Pythagorean theorem to calculate the length of a side.
                   Since we're just searching for a longest one, there's no
                   need to calculate sqrt(), as comparison works equally well
                   for squares.
                 */
                int length_x = (int)side_start.X - (int)side_end.X;
                int length_y = (int)side_start.Y - (int)side_end.Y;

                int sideSquared = length_x * length_x + length_y * length_y;

                if (sideSquared > longestSideSquared)
                {
                    longestSideSquared = sideSquared;
                    result = plot;
                }
            }
        }

        // there's an issue with empty set, not covered by API properly
        return result.Value;
    }
}
