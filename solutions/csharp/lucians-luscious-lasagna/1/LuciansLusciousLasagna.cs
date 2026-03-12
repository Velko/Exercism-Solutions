class Lasagna
{
    public int ExpectedMinutesInOven() => 40;

    public int RemainingMinutesInOven(int elapsedMinutesInOven) => ExpectedMinutesInOven() - elapsedMinutesInOven;

    public int PreparationTimeInMinutes(int layers) => layers * 2;

    public int ElapsedTimeInMinutes(int layers, int elapsedMinutesInOven) => PreparationTimeInMinutes(layers) + elapsedMinutesInOven;
}
