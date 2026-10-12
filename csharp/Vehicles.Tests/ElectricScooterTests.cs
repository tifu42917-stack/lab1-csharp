namespace Vehicles.Tests;

[TestClass]
public class ElectricScooterTests
{
    private static ElectricScooter CreateXiaomi() =>
        new("Xiaomi", "Black", 45_000m, 25.0, 0.474, 45.0);

    [TestMethod]
    public void DefaultConstructor_SetsDefaultValues()
    {
        var scooter = new ElectricScooter();

        Assert.AreEqual(Vehicle.DefaultBrand, scooter.Brand);
        Assert.AreEqual(ElectricScooter.DefaultColor, scooter.Color);
        Assert.AreEqual(ElectricScooter.DefaultPrice, scooter.Price);
        Assert.AreEqual(ElectricScooter.DefaultMaxSpeedKmh, scooter.MaxSpeedKmh);
        Assert.AreEqual(ElectricScooter.DefaultBatteryKWh, scooter.BatteryKWh);
        Assert.AreEqual(ElectricScooter.DefaultRangeKm, scooter.RangeKm);
    }

    [TestMethod]
    public void Constructor_SetsOwnAndInheritedProperties()
    {
        var scooter = CreateXiaomi();

        Assert.AreEqual("Xiaomi", scooter.Brand);
        Assert.AreEqual("Black", scooter.Color);
        Assert.AreEqual(45_000m, scooter.Price);
        Assert.AreEqual(25.0, scooter.MaxSpeedKmh);
        Assert.AreEqual(0.474, scooter.BatteryKWh);
        Assert.AreEqual(45.0, scooter.RangeKm);
    }

    [TestMethod]
    public void Properties_CanBeChanged()
    {
        var scooter = CreateXiaomi();

        scooter.MaxSpeedKmh = 30;
        scooter.BatteryKWh = 0.6;
        scooter.RangeKm = 60;

        Assert.AreEqual(30.0, scooter.MaxSpeedKmh);
        Assert.AreEqual(0.6, scooter.BatteryKWh);
        Assert.AreEqual(60.0, scooter.RangeKm);
    }

    [TestMethod]
    [DataRow(0.0)]
    [DataRow(-10.0)]
    [DataRow(double.NaN)]
    public void MaxSpeed_Invalid_Throws(double speed)
    {
        var scooter = CreateXiaomi();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => scooter.MaxSpeedKmh = speed);
        Assert.AreEqual(25.0, scooter.MaxSpeedKmh, "Старое значение должно сохраниться");
    }

    [TestMethod]
    public void Battery_NotPositive_Throws()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => new ElectricScooter("Xiaomi", "Black", 45_000m, 25.0, 0, 45.0));
    }

    [TestMethod]
    public void Range_NotPositive_Throws()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => new ElectricScooter("Xiaomi", "Black", 45_000m, 25.0, 0.5, -1));
    }

    [TestMethod]
    public void InheritedValidation_Works()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => new ElectricScooter("Xiaomi", "Black", 0m, 25.0, 0.5, 45.0));
    }

    [TestMethod]
    public void Kind_IsOverridden()
    {
        Assert.AreEqual("Электросамокат", new ElectricScooter().Kind);
    }

    [TestMethod]
    public void GetDescription_ContainsBaseAndOwnFields()
    {
        string description = CreateXiaomi().GetDescription();

        StringAssert.StartsWith(description, "[Электросамокат]");
        StringAssert.Contains(description, "Марка: Xiaomi");
        StringAssert.Contains(description, "Цена: 45000.00 руб.");
        StringAssert.Contains(description, "Макс. скорость: 25 км/ч");
        StringAssert.Contains(description, "Ёмкость батареи: 0.474 кВт·ч");
        StringAssert.Contains(description, "Запас хода: 45 км");
    }
}
