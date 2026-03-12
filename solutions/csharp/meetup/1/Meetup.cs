using System;

public enum Schedule
{
    Teenth,
    First,
    Second,
    Third,
    Fourth,
    Last
}

public class Meetup
{
    DateTime startOfTheMonth;
    public Meetup(int month, int year)
    {
        startOfTheMonth = new DateTime(year, month, 1);
    }

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule)
    {
        var daysOffset = dayOfWeek - startOfTheMonth.DayOfWeek;

        if (daysOffset < 0) daysOffset += 7;

        var daysInCurrentMonth = DateTime.DaysInMonth(startOfTheMonth.Year, startOfTheMonth.Month);

        var weeksOffset = schedule switch
        {
            Schedule.First => 0,
            Schedule.Second => 1,
            Schedule.Third => 2,
            Schedule.Fourth => 3,
            Schedule.Teenth when daysOffset > 4 => 1,
            Schedule.Teenth => 2,
            Schedule.Last when daysOffset + 28 < daysInCurrentMonth => 4,
            Schedule.Last when daysOffset + 21 < daysInCurrentMonth => 3,
            Schedule.Last => 5,
            _ => throw new ArgumentException(),
        };

        var result = startOfTheMonth.AddDays(daysOffset + weeksOffset * 7);

        return result;
    }
}