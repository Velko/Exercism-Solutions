class RemoteControlCar
{
    int odometer = 0;
    int batteryLevel = 100; 

    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }

    public string DistanceDisplay() => $"Driven {odometer} meters";

    public string BatteryDisplay() => "Battery " +
        (batteryLevel > 0
         ? $"at {batteryLevel}%"
         : "empty");

    public void Drive()
    {
        if (batteryLevel > 0)
        {
            odometer += 20;
            batteryLevel -= 1;
        }
    }
}
