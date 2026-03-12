using System;

public class SpaceAge
{
    const double SECONDS_IN_EARTH_YEAR = 31557600;

    double ageInEarthYears;

    public SpaceAge(int seconds)
    {
        ageInEarthYears = seconds / SECONDS_IN_EARTH_YEAR;
    }

    public double OnEarth() => ageInEarthYears;

    public double OnMercury() => ageInEarthYears / 0.2408467;

    public double OnVenus() => ageInEarthYears / 0.61519726;

    public double OnMars() => ageInEarthYears / 1.8808158;

    public double OnJupiter() => ageInEarthYears / 11.862615;

    public double OnSaturn() => ageInEarthYears / 29.447498;

    public double OnUranus() => ageInEarthYears / 84.016846;

    public double OnNeptune() => ageInEarthYears / 164.79132;
}