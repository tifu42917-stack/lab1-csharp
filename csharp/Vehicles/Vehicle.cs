using System.Globalization;
using System.Text;

namespace Vehicles;

public class Vehicle
{
    public const string DefaultBrand = "Unknown";
    public const string DefaultColor = "White";
    public const decimal DefaultPrice = 100_000m;

    protected static readonly CultureInfo Format = CultureInfo.InvariantCulture;

    private string _brand = DefaultBrand;
    private string _color = DefaultColor;
    private decimal _price = DefaultPrice;

    public Vehicle()
        : this(DefaultBrand, DefaultColor, DefaultPrice)
    {
    }

    public Vehicle(string brand, string color, decimal price)
    {
        Brand = brand;
        Color = color;
        Price = price;
    }

    public string Brand
    {
        get => _brand;
        set => _brand = RequireText(value, "Марка не может быть пустой");
    }

    public string Color
    {
        get => _color;
        set => _color = RequireText(value, "Цвет не может быть пустым");
    }

    public decimal Price
    {
        get => _price;
        set => _price = value > 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), value, "Цена должна быть больше нуля");
    }

    public virtual string Kind => "Транспортное средство";

    public virtual string GetDescription()
    {
        var sb = new StringBuilder();
        sb.Append('[').Append(Kind).Append(']');
        sb.AppendLine().Append("  Марка: ").Append(Brand);
        sb.AppendLine().Append("  Цвет: ").Append(Color);
        sb.AppendLine().Append("  Цена: ").Append(Price.ToString("0.00", Format)).Append(" руб.");
        return sb.ToString();
    }

    public override string ToString() => GetDescription();

    protected static string RequireText(string? value, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(errorMessage, nameof(value));
        }
        return value.Trim();
    }

    protected static double RequirePositive(double value, string errorMessage)
    {
        if (value <= 0 || double.IsNaN(value) || double.IsInfinity(value))
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, errorMessage);
        }
        return value;
    }
}
