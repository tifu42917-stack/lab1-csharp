using System.Globalization;
using System.Text;
using Vehicles;

namespace Vehicles.App;

internal static class Program
{
    private enum MenuItem
    {
        Exit = 0,
        AddVehicle = 1,
        AddScooter = 2,
        AddCar = 3,
        PrintList = 4,
    }

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("--- Проверка виртуального метода GetDescription() ---");
        Console.WriteLine();
        DemonstratePolymorphism();

        Console.WriteLine("--- Список объектов ---");
        Console.WriteLine();
        RunMenu();

        Console.WriteLine("Программа завершена.");
    }

    private static void DemonstratePolymorphism()
    {
        Vehicle[] vehicles =
        {
            new Vehicle("Generic", "Gray", 500_000m),
            new ElectricScooter("Xiaomi", "Black", 45_000m, 25.0, 0.474, 45.0),
            new Car("Toyota", "Silver", 2_850_000m, "Camry", 2023, 2.5),
        };

        foreach (Vehicle vehicle in vehicles)
        {
            Console.WriteLine($"Vehicle -> {vehicle.GetType().Name}:");
            Console.WriteLine(vehicle.GetDescription());
            Console.WriteLine();
        }
    }

    private static void RunMenu()
    {
        var vehicles = new List<Vehicle>();

        try
        {
            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("  1 - Добавить транспортное средство");
                Console.WriteLine("  2 - Добавить электросамокат");
                Console.WriteLine("  3 - Добавить легковой автомобиль");
                Console.WriteLine("  4 - Вывести список");
                Console.WriteLine("  0 - Завершить");

                var choice = (MenuItem)ReadInt("Выбор: ");
                if (choice == MenuItem.Exit)
                {
                    return;
                }

                try
                {
                    switch (choice)
                    {
                        case MenuItem.AddVehicle:
                            vehicles.Add(CreateVehicle());
                            break;
                        case MenuItem.AddScooter:
                            vehicles.Add(CreateScooter());
                            break;
                        case MenuItem.AddCar:
                            vehicles.Add(CreateCar());
                            break;
                        case MenuItem.PrintList:
                            PrintList(vehicles);
                            continue;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            Console.WriteLine();
                            continue;
                    }
                    Console.WriteLine("Добавлено.");
                    Console.WriteLine();
                }
                catch (ArgumentException error)
                {
                    Console.WriteLine($"Ошибка: {FirstLine(error.Message)}");
                    Console.WriteLine();
                }
            }
        }
        catch (EndOfStreamException)
        {
        }
    }

    private static void PrintList(List<Vehicle> vehicles)
    {
        if (vehicles.Count == 0)
        {
            Console.WriteLine("Список пуст.");
            Console.WriteLine();
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Список объектов ({vehicles.Count}):");
        Console.WriteLine();
        for (int i = 0; i < vehicles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {vehicles[i]}");
            Console.WriteLine();
        }
    }

    private static Vehicle CreateVehicle()
    {
        string brand = ReadText("  Марка: ");
        string color = ReadText("  Цвет: ");
        decimal price = ReadPositiveDecimal("  Цена (руб.): ");
        return new Vehicle(brand, color, price);
    }

    private static ElectricScooter CreateScooter()
    {
        string brand = ReadText("  Марка: ");
        string color = ReadText("  Цвет: ");
        decimal price = ReadPositiveDecimal("  Цена (руб.): ");
        double maxSpeed = ReadPositiveDouble("  Макс. скорость (км/ч): ");
        double battery = ReadPositiveDouble("  Ёмкость батареи (кВт·ч): ");
        double range = ReadPositiveDouble("  Запас хода (км): ");
        return new ElectricScooter(brand, color, price, maxSpeed, battery, range);
    }

    private static Car CreateCar()
    {
        string brand = ReadText("  Марка: ");
        string color = ReadText("  Цвет: ");
        decimal price = ReadPositiveDecimal("  Цена (руб.): ");
        string model = ReadText("  Модель: ");
        int year = ReadInt("  Год выпуска: ");
        double engine = ReadPositiveDouble("  Объём двигателя (л): ");
        return new Car(brand, color, price, model, year, engine);
    }

    private static string ReadLine(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? throw new EndOfStreamException();
    }

    private static string ReadText(string prompt)
    {
        while (true)
        {
            string value = ReadLine(prompt).Trim();
            if (value.Length > 0)
            {
                return value;
            }
            Console.WriteLine("  Значение не может быть пустым.");
        }
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            if (int.TryParse(ReadLine(prompt).Trim(), out int value))
            {
                return value;
            }
            Console.WriteLine("  Введите целое число.");
        }
    }

    private static double ReadPositiveDouble(string prompt)
    {
        while (true)
        {
            if (double.TryParse(NormalizeNumber(ReadLine(prompt)), NumberStyles.Float,
                    CultureInfo.InvariantCulture, out double value) && value > 0)
            {
                return value;
            }
            Console.WriteLine("  Введите положительное число.");
        }
    }

    private static decimal ReadPositiveDecimal(string prompt)
    {
        while (true)
        {
            if (decimal.TryParse(NormalizeNumber(ReadLine(prompt)), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out decimal value) && value > 0)
            {
                return value;
            }
            Console.WriteLine("  Введите положительное число.");
        }
    }

    private static string NormalizeNumber(string text) => text.Trim().Replace(',', '.');

    private static string FirstLine(string message) => message.Split('\n')[0].Split(" (Parameter")[0];
}
