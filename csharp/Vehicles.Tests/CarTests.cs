namespace Vehicles.Tests;

[TestClass]
public class CarTests
{
    private static Car CreateCamry() =>
        new("Toyota", "Silver", 2_850_000m, "Camry", 2023, 2.5);

    [TestMethod]
    public void DefaultConstructor_SetsDefaultValues()
    {
        var car = new Car();

        Assert.AreEqual(Vehicle.DefaultBrand, car.Brand);
        Assert.AreEqual(Vehicle.DefaultColor, car.Color);
        Assert.AreEqual(Car.DefaultPrice, car.Price);
        Assert.AreEqual(Car.DefaultModel, car.Model);
        Assert.AreEqual(Car.DefaultYear, car.Year);
        Assert.AreEqual(Car.DefaultEngineVolumeL, car.EngineVolumeL);
    }

    [TestMethod]
    public void Constructor_SetsOwnAndInheritedProperties()
    {
        var car = CreateCamry();

        Assert.AreEqual("Toyota", car.Brand);
        Assert.AreEqual("Silver", car.Color);
        Assert.AreEqual(2_850_000m, car.Price);
        Assert.AreEqual("Camry", car.Model);
        Assert.AreEqual(2023, car.Year);
        Assert.AreEqual(2.5, car.EngineVolumeL);
    }

    [TestMethod]
    public void Properties_CanBeChanged()
    {
        var car = CreateCamry();

        car.Model = "Corolla";
        car.Year = 2019;
        car.EngineVolumeL = 1.8;
        car.Price = 1_900_000m;

        Assert.AreEqual("Corolla", car.Model);
        Assert.AreEqual(2019, car.Year);
        Assert.AreEqual(1.8, car.EngineVolumeL);
        Assert.AreEqual(1_900_000m, car.Price);
    }

    [TestMethod]
    public void Year_MinYear_IsAccepted()
    {
        var car = CreateCamry();

        car.Year = Car.MinYear;

        Assert.AreEqual(Car.MinYear, car.Year);
    }

    [TestMethod]
    [DataRow(Car.MinYear - 1)]
    [DataRow(0)]
    [DataRow(-2023)]
    public void Year_BeforeMinYear_Throws(int year)
    {
        var car = CreateCamry();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => car.Year = year);
        Assert.AreEqual(2023, car.Year, "Старое значение должно сохраниться");
    }

    [TestMethod]
    [DataRow(0.0)]
    [DataRow(-1.6)]
    public void EngineVolume_NotPositive_Throws(double volume)
    {
        var car = CreateCamry();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => car.EngineVolumeL = volume);
    }

    [TestMethod]
    public void Model_Empty_Throws()
    {
        Assert.ThrowsException<ArgumentException>(
            () => new Car("Toyota", "Silver", 1m, " ", 2023, 2.5));
    }

    [TestMethod]
    public void Kind_IsOverridden()
    {
        Assert.AreEqual("Легковой автомобиль", new Car().Kind);
    }

    [TestMethod]
    public void GetDescription_ContainsBaseAndOwnFields()
    {
        string description = CreateCamry().GetDescription();

        StringAssert.StartsWith(description, "[Легковой автомобиль]");
        StringAssert.Contains(description, "Марка: Toyota");
        StringAssert.Contains(description, "Цвет: Silver");
        StringAssert.Contains(description, "Цена: 2850000.00 руб.");
        StringAssert.Contains(description, "Модель: Camry");
        StringAssert.Contains(description, "Год выпуска: 2023");
        StringAssert.Contains(description, "Объём двигателя: 2.5 л");
    }
}
