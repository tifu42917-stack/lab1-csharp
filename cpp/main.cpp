#include <iostream>
#include <list>
#include <memory>
#include <stdexcept>
#include <string>

#include "Car.h"
#include "ElectricScooter.h"
#include "Vehicle.h"

namespace {

enum MenuItem {
    kExit = 0,
    kAddVehicle = 1,
    kAddScooter = 2,
    kAddCar = 3,
    kPrintList = 4
};

std::string readLine(const std::string& prompt) {
    std::cout << prompt;
    std::string line;
    if (!std::getline(std::cin, line))
        throw std::runtime_error("Ввод завершён");
    return line;
}

std::string readNonEmpty(const std::string& prompt) {
    while (true) {
        std::string value = readLine(prompt);
        if (!value.empty())
            return value;
        std::cout << "  Значение не может быть пустым.\n";
    }
}

int readInt(const std::string& prompt) {
    while (true) {
        std::string line = readLine(prompt);
        try {
            std::size_t pos = 0;
            int value = std::stoi(line, &pos);
            if (pos == line.size())
                return value;
        } catch (const std::exception&) {
        }
        std::cout << "  Введите целое число.\n";
    }
}

double readPositiveDouble(const std::string& prompt) {
    while (true) {
        std::string line = readLine(prompt);
        for (char& ch : line)
            if (ch == ',') ch = '.';
        try {
            std::size_t pos = 0;
            double value = std::stod(line, &pos);
            if (pos == line.size() && value > 0)
                return value;
        } catch (const std::exception&) {
        }
        std::cout << "  Введите положительное число.\n";
    }
}

std::unique_ptr<Vehicle> createVehicle() {
    std::string brand = readNonEmpty("  Марка: ");
    std::string color = readNonEmpty("  Цвет: ");
    double price = readPositiveDouble("  Цена (руб.): ");
    return std::make_unique<Vehicle>(brand, color, price);
}

std::unique_ptr<Vehicle> createScooter() {
    std::string brand = readNonEmpty("  Марка: ");
    std::string color = readNonEmpty("  Цвет: ");
    double price = readPositiveDouble("  Цена (руб.): ");
    double maxSpeed = readPositiveDouble("  Макс. скорость (км/ч): ");
    double battery = readPositiveDouble("  Ёмкость батареи (кВт·ч): ");
    double range = readPositiveDouble("  Запас хода (км): ");
    return std::make_unique<ElectricScooter>(brand, color, price, maxSpeed, battery, range);
}

std::unique_ptr<Vehicle> createCar() {
    std::string brand = readNonEmpty("  Марка: ");
    std::string color = readNonEmpty("  Цвет: ");
    double price = readPositiveDouble("  Цена (руб.): ");
    std::string model = readNonEmpty("  Модель: ");
    int year = readInt("  Год выпуска: ");
    double engine = readPositiveDouble("  Объём двигателя (л): ");
    return std::make_unique<Car>(brand, color, price, model, year, engine);
}

void demonstratePolymorphism() {
    Vehicle vehicle("Generic", "Gray", 500000.0);
    ElectricScooter scooter("Xiaomi", "Black", 45000.0, 25.0, 0.474, 45.0);
    Car car("Toyota", "Silver", 2850000.0, "Camry", 2023, 2.5);

    Vehicle* ptr = &vehicle;
    std::cout << "Vehicle* -> Vehicle:\n" << ptr->toString() << "\n\n";

    ptr = &scooter;
    std::cout << "Vehicle* -> ElectricScooter:\n" << ptr->toString() << "\n\n";

    const Vehicle& ref = car;
    std::cout << "Vehicle& -> Car (через operator<<):\n" << ref << "\n\n";
}

void printList(const std::list<std::unique_ptr<Vehicle>>& vehicles) {
    if (vehicles.empty()) {
        std::cout << "Список пуст.\n\n";
        return;
    }
    std::cout << "\nСписок объектов (" << vehicles.size() << "):\n\n";
    int number = 1;
    for (const auto& vehicle : vehicles)
        std::cout << number++ << ". " << *vehicle << "\n\n";
}

}

int main() {
    std::cout << "--- Проверка виртуальной функции toString() ---\n\n";
    demonstratePolymorphism();

    std::cout << "--- Список объектов ---\n\n";
    std::list<std::unique_ptr<Vehicle>> vehicles;

    try {
        while (true) {
            std::cout << "Меню:\n"
                      << "  1 - Добавить транспортное средство\n"
                      << "  2 - Добавить электросамокат\n"
                      << "  3 - Добавить легковой автомобиль\n"
                      << "  4 - Вывести список\n"
                      << "  0 - Завершить\n";
            int choice = readInt("Выбор: ");
            if (choice == kExit)
                break;

            try {
                switch (choice) {
                    case kAddVehicle: vehicles.push_back(createVehicle()); break;
                    case kAddScooter: vehicles.push_back(createScooter()); break;
                    case kAddCar:     vehicles.push_back(createCar()); break;
                    case kPrintList:  printList(vehicles); continue;
                    default:
                        std::cout << "Неверный выбор.\n\n";
                        continue;
                }
                std::cout << "Добавлено.\n\n";
            } catch (const std::invalid_argument& error) {
                std::cout << "Ошибка: " << error.what() << "\n\n";
            }
        }
    } catch (const std::runtime_error&) {
    }

    std::cout << "Программа завершена.\n";
    return 0;
}
