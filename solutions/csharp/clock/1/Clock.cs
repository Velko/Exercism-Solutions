using System;

public class Clock
{
    int minutes_total;

    const int MINUTES_IN_HOUR = 60;
    const int MINUTES_IN_DAY = 24 * MINUTES_IN_HOUR;

    public Clock(int hours, int minutes)
    {
        minutes_total = (
            ((hours * MINUTES_IN_HOUR + minutes) % MINUTES_IN_DAY)
                + MINUTES_IN_DAY) % MINUTES_IN_DAY;
    }

    public Clock Add(int minutesToAdd)
        => new Clock(0, minutes_total + minutesToAdd);

    public Clock Subtract(int minutesToSubtract)
        => new Clock(0, minutes_total - minutesToSubtract);

    public override string ToString()
        => $"{minutes_total / MINUTES_IN_HOUR:D02}:{minutes_total % MINUTES_IN_HOUR:D02}";

    public override bool Equals(object obj)
        => minutes_total == (obj as Clock)?.minutes_total;

    public override int GetHashCode()
        => minutes_total.GetHashCode();
}
