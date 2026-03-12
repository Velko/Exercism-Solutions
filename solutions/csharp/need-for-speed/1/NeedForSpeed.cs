using System;

class RemoteControlCar
{
    private int _speed;
    private int _batteryDrain;
    private int _distanceDriven;
    private int _batteryChargeRemaining;

    public RemoteControlCar(int speed, int batteryDrain)
    {
        _speed = speed;
        _batteryDrain = batteryDrain;
        _distanceDriven = 0;
        _batteryChargeRemaining = 100;
    }

    public bool BatteryDrained() => _batteryChargeRemaining < _batteryDrain;

    public int DistanceDriven() => _distanceDriven;

    public void Drive()
    {
        if (!BatteryDrained())
        {
            _distanceDriven += _speed;
            _batteryChargeRemaining -= _batteryDrain;
        }
    }

    public static RemoteControlCar Nitro() => new RemoteControlCar(50, 4);
}

class RaceTrack
{
    private int _distance;

    public RaceTrack(int distance)
    {
        _distance = distance;
    }

    public bool TryFinishTrack(RemoteControlCar car)
    {
        while (!car.BatteryDrained() && car.DistanceDriven() < _distance)
            car.Drive();

        return car.DistanceDriven() >= _distance;
    }
}
