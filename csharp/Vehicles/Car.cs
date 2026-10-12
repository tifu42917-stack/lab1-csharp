namespace Vehicles;

public class Car : Vehicle
{
    public new const decimal DefaultPrice = 1_000_000m;
    public const string DefaultModel = "Unknown";
    public const int DefaultYear = 2020;
    public const double DefaultEngineVolumeL = 1.6;

    public const int MinYear = 1886;

    private string _model = DefaultModel;
    private int _year = DefaultYear;
    private double _engineVolumeL = DefaultEngineVolumeL;

    public Car()
        : this(DefaultBrand, DefaultColor, DefaultPrice,
               DefaultModel, DefaultYear, DefaultEngineVolumeL)
    {
    }

    public Car(string brand, string color, decimal price,
               string model, int year, double engineVolumeL)
        : base(brand, color, price)
    {
        Model = model;
        Year = year;
        EngineVolumeL = engineVolumeL;
    }

    public string Model
    {
        get => _model;
        set => _model = RequireText(value, "Модель не может быть пустой");
    }

    public int Year
    {
        get => _year;
        set => _year = value >= MinYear
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), value,
                $"Год выпуска не может быть раньше {MinYear}");
    }

    public double EngineVolumeL
    {
        get => _engineVolumeL;
        set => _engineVolumeL = RequirePositive(value, "Объём двигателя должен быть больше нуля");
    }

    public override string Kind => "Легковой автомобиль";

    public override string GetDescription() =>
        base.GetDescription()
        + Environment.NewLine + "  Модель: " + Model
        + Environment.NewLine + "  Год выпуска: " + Year.ToString(Format)
        + Environment.NewLine + "  Объём двигателя: " + EngineVolumeL.ToString(Format) + " л";
}
