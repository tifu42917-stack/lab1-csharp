namespace Vehicles.Tests;

[TestClass]
public class PolymorphismTests
{
    [TestMethod]
    public void DerivedObjects_AreVehicles()
    {
        Assert.IsInstanceOfType(new ElectricScooter(), typeof(Vehicle));
        Assert.IsInstanceOfType(new Car(), typeof(Vehicle));
    }

    [TestMethod]
    public void VirtualMethod_CalledThroughBaseReference_UsesRealType()
    {
        Vehicle vehicle = new Car("Toyota", "Silver", 2_850_000m, "Camry", 2023, 2.5);

        string description = vehicle.GetDescription();

        StringAssert.StartsWith(description, "[Легковой автомобиль]");
        StringAssert.Contains(description, "Модель: Camry");
    }

    [TestMethod]
    public void List_OfBaseType_CallsOverriddenMembers()
    {
        var vehicles = new List<Vehicle>
        {
            new Vehicle(),
            new ElectricScooter(),
            new Car(),
        };

        string[] kinds = vehicles.Select(v => v.Kind).ToArray();

        CollectionAssert.AreEqual(
            new[] { "Транспортное средство", "Электросамокат", "Легковой автомобиль" },
            kinds);
    }

    [TestMethod]
    public void List_OfBaseType_EachDescriptionHasOwnFields()
    {
        var vehicles = new List<Vehicle>
        {
            new ElectricScooter("Xiaomi", "Black", 45_000m, 25.0, 0.474, 45.0),
            new Car("Toyota", "Silver", 2_850_000m, "Camry", 2023, 2.5),
        };

        StringAssert.Contains(vehicles[0].GetDescription(), "Запас хода");
        StringAssert.Contains(vehicles[1].GetDescription(), "Объём двигателя");
        Assert.IsFalse(vehicles[0].GetDescription().Contains("Модель"));
    }

    [TestMethod]
    public void ToString_UsesOverriddenDescription()
    {
        Vehicle vehicle = new ElectricScooter();

        Assert.AreEqual(vehicle.GetDescription(), vehicle.ToString());
        StringAssert.StartsWith(vehicle.ToString(), "[Электросамокат]");
    }
}
