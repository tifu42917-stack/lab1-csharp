#include "Car.h"

#include <sstream>
#include <stdexcept>

Car::Car()
    : Car(kDefaultBrand, kDefaultColor, kDefaultPrice,
          kDefaultModel, kDefaultYear, kDefaultEngineVolumeL) {}

Car::Car(const std::string& brand, const std::string& color, double price,
         const std::string& model, int year, double engineVolumeL)
    : Vehicle(brand, color, price),
      year_(kDefaultYear),
      engineVolumeL_(kDefaultEngineVolumeL) {
    setModel(model);
    setYear(year);
    setEngineVolumeL(engineVolumeL);
}

std::string Car::kind() const {
    return "Легковой автомобиль";
}

std::string Car::toString() const {
    std::ostringstream oss;
    oss << Vehicle::toString()
        << "\n  Модель: " << model_
        << "\n  Год выпуска: " << year_
        << "\n  Объём двигателя: " << engineVolumeL_ << " л";
    return oss.str();
}

const std::string& Car::getModel() const { return model_; }
int Car::getYear() const { return year_; }
double Car::getEngineVolumeL() const { return engineVolumeL_; }

void Car::setModel(const std::string& model) {
    if (model.empty())
        throw std::invalid_argument("Модель не может быть пустой");
    model_ = model;
}

void Car::setYear(int year) {
    if (year < kMinYear)
        throw std::invalid_argument("Год выпуска не может быть раньше " + std::to_string(kMinYear));
    year_ = year;
}

void Car::setEngineVolumeL(double engineVolumeL) {
    if (engineVolumeL <= 0)
        throw std::invalid_argument("Объём двигателя должен быть больше нуля");
    engineVolumeL_ = engineVolumeL;
}
