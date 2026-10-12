namespace Vehicles.Tests;

[TestClass]
public class VehicleTests
{
    [TestMethod]
    public void DefaultConstructor_SetsDefaultValues()
    {
        var vehicle = new Vehicle();

        Assert.AreEqual(Vehicle.DefaultBrand, vehicle.Brand);
        Assert.AreEqual(Vehicle.DefaultColor, vehicle.Color);
        Assert.AreEqual(Vehicle.DefaultPrice, vehicle.Price);
    }

    [TestMethod]
    public void Constructor_SetsAllProperties()
    {
        var vehicle = new Vehicle("Generic", "Gray", 500_000m);

        Assert.AreEqual("Generic", vehicle.Brand);
        Assert.AreEqual("Gray", vehicle.Color);
        Assert.AreEqual(500_000m, vehicle.Price);
    }

    [TestMethod]
    public void Properties_CanBeChanged()
    {
        var vehicle = new Vehicle();

        vehicle.Brand = "Volvo";
        vehicle.Color = "Red";
        vehicle.Price = 123.45m;

        Assert.AreEqual("Volvo", vehicle.Brand);
        Assert.AreEqual("Red", vehicle.Color);
        Assert.AreEqual(123.45m, vehicle.Price);
    }

    [TestMethod]
    public void Brand_IsTrimmed()
    {
        var vehicle = new Vehicle("  Volvo  ", "Red", 1m);

        Assert.AreEqual("Volvo", vehicle.Brand);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void Price_NotPositive_Throws(int price)
    {
        var vehicle = new Vehicle();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => vehicle.Price = price);
        Assert.AreEqual(Vehicle.DefaultPrice, vehicle.Price, "Старое значение должно сохраниться");
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("   ")]
    public void Brand_Empty_Throws(string brand)
    {
        var vehicle = new Vehicle();

        Assert.ThrowsException<ArgumentException>(() => vehicle.Brand = brand);
    }

    [TestMethod]
    public void Brand_Null_Throws()
    {
        var vehicle = new Vehicle();

        Assert.ThrowsException<ArgumentException>(() => vehicle.Brand = null!);
    }

    [TestMethod]
    public void Color_Empty_Throws()
    {
        Assert.ThrowsException<ArgumentException>(() => new Vehicle("Volvo", "", 1m));
    }

    [TestMethod]
    public void Kind_ReturnsBaseName()
    {
        Assert.AreEqual("Транспортное средство", new Vehicle().Kind);
    }

    [TestMethod]
    public void GetDescription_ContainsAllFields()
    {
        var vehicle = new Vehicle("Generic", "Gray", 500_000m);

        string description = vehicle.GetDescription();

        StringAssert.StartsWith(description, "[Транспортное средство]");
        StringAssert.Contains(description, "Марка: Generic");
        StringAssert.Contains(description, "Цвет: Gray");
        StringAssert.Contains(description, "Цена: 500000.00 руб.");
    }

    [TestMethod]
    public void ToString_EqualsGetDescription()
    {
        var vehicle = new Vehicle("Generic", "Gray", 500_000m);

        Assert.AreEqual(vehicle.GetDescription(), vehicle.ToString());
    }
}
