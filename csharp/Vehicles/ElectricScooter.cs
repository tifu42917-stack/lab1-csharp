namespace Vehicles;

public class ElectricScooter : Vehicle
{
    public new const string DefaultColor = "Black";
    public new const decimal DefaultPrice = 30_000m;
    public const double DefaultMaxSpeedKmh = 25.0;
    public const double DefaultBatteryKWh = 0.5;
    public const double DefaultRangeKm = 30.0;

    private double _maxSpeedKmh = DefaultMaxSpeedKmh;
    private double _batteryKWh = DefaultBatteryKWh;
    private double _rangeKm = DefaultRangeKm;

    public ElectricScooter()
        : this(DefaultBrand, DefaultColor, DefaultPrice,
               DefaultMaxSpeedKmh, DefaultBatteryKWh, DefaultRangeKm)
    {
    }

    public ElectricScooter(string brand, string color, decimal price,
                           double maxSpeedKmh, double batteryKWh, double rangeKm)
        : base(brand, color, price)
    {
        MaxSpeedKmh = maxSpeedKmh;
        BatteryKWh = batteryKWh;
        RangeKm = rangeKm;
    }

    public double MaxSpeedKmh
    {
        get => _maxSpeedKmh;
        set => _maxSpeedKmh = RequirePositive(value, "Максимальная скорость должна быть больше нуля");
    }

    public double BatteryKWh
    {
        get => _batteryKWh;
        set => _batteryKWh = RequirePositive(value, "Ёмкость батареи должна быть больше нуля");
    }

    public double RangeKm
    {
        get => _rangeKm;
        set => _rangeKm = RequirePositive(value, "Запас хода должен быть больше нуля");
    }

    public override string Kind => "Электросамокат";

    public override string GetDescription() =>
        base.GetDescription()
        + Environment.NewLine + "  Макс. скорость: " + MaxSpeedKmh.ToString(Format) + " км/ч"
        + Environment.NewLine + "  Ёмкость батареи: " + BatteryKWh.ToString(Format) + " кВт·ч"
        + Environment.NewLine + "  Запас хода: " + RangeKm.ToString(Format) + " км";
}
