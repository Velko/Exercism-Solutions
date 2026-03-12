using System;

class BirdCount
{
    private int[] birdsPerDay;

    public BirdCount(int[] birdsPerDay)
    {
        this.birdsPerDay = birdsPerDay;
    }

    public static int[] LastWeek() => new[] { 0, 2, 5, 3, 7, 8, 4 };

    public int Today() => birdsPerDay[birdsPerDay.Length - 1];

    public void IncrementTodaysCount()
    {
        ++birdsPerDay[birdsPerDay.Length - 1];
    }

    public bool HasDayWithoutBirds()
    {
        foreach (var dayCount in birdsPerDay)
            if (dayCount == 0)
                return true;

        return false;
    }

    public int CountForFirstDays(int numberOfDays)
    {
        var birdsTotal = 0;
        for (int i = 0; i < numberOfDays; ++i)
            birdsTotal += birdsPerDay[i];

        return birdsTotal;
    }

    public int BusyDays()
    {
        var busyDays = 0;
        foreach (var dayCount in birdsPerDay)
            if (dayCount >= 5)
                ++busyDays;

        return busyDays;
    }
}
