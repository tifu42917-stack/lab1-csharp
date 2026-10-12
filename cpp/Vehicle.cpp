#include "Vehicle.h"

#include <iomanip>
#include <sstream>
#include <stdexcept>

Vehicle::Vehicle()
    : Vehicle(kDefaultBrand, kDefaultColor, kDefaultPrice) {}

Vehicle::Vehicle(const std::string& brand, const std::string& color, double price)
    : price_(kDefaultPrice) {
    setBrand(brand);
    setColor(color);
    setPrice(price);
}

std::string Vehicle::kind() const {
    return "Транспортное средство";
}

std::string Vehicle::toString() const {
    std::ostringstream oss;
    oss << "[" << kind() << "]"
        << "\n  Марка: " << brand_
        << "\n  Цвет: " << color_
        << "\n  Цена: " << std::fixed << std::setprecision(2) << price_ << " руб.";
    return oss.str();
}

const std::string& Vehicle::getBrand() const { return brand_; }
const std::string& Vehicle::getColor() const { return color_; }
double Vehicle::getPrice() const { return price_; }

void Vehicle::setBrand(const std::string& brand) {
    if (brand.empty())
        throw std::invalid_argument("Марка не может быть пустой");
    brand_ = brand;
}

void Vehicle::setColor(const std::string& color) {
    if (color.empty())
        throw std::invalid_argument("Цвет не может быть пустым");
    color_ = color;
}

void Vehicle::setPrice(double price) {
    if (price <= 0)
        throw std::invalid_argument("Цена должна быть больше нуля");
    price_ = price;
}

std::ostream& operator<<(std::ostream& out, const Vehicle& vehicle) {
    return out << vehicle.toString();
}
