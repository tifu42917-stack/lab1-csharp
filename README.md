Лабораторная работа 1. Знакомство с C#

Вариант 9: базовый класс «Транспортное средство» (Vehicle),
производные классы «Электросамокат» (ElectricScooter) и «Легковой автомобиль» (Car).

Структура:
  cpp/                    исправленная программа из РГР на C++
  csharp/Lab1.sln         решение C#
  csharp/Vehicles/        классы Vehicle, ElectricScooter, Car
  csharp/VehiclesApp/     консольное приложение
  csharp/Vehicles.Tests/  модульные тесты MSTest

Запуск C#:
  cd csharp
  dotnet run --project VehiclesApp
  dotnet test

Запуск C++:
  cd cpp
  clang++ -std=c++17 *.cpp -o vehicles
  ./vehicles
