#include "ElectricScooter.h"

#include <sstream>
#include <stdexcept>

ElectricScooter::ElectricScooter()
    : ElectricScooter(kDefaultBrand, kDefaultColor, kDefaultPrice,
                      kDefaultMaxSpeedKmh, kDefaultBatteryKWh, kDefaultRangeKm) {}

ElectricScooter::ElectricScooter(const std::string& brand, const std::string& color, double price,
                                 double maxSpeedKmh, double batteryKWh, double rangeKm)
    : Vehicle(brand, color, price),
      maxSpeedKmh_(kDefaultMaxSpeedKmh),
      batteryKWh_(kDefaultBatteryKWh),
      rangeKm_(kDefaultRangeKm) {
    setMaxSpeedKmh(maxSpeedKmh);
    setBatteryKWh(batteryKWh);
    setRangeKm(rangeKm);
}

std::string ElectricScooter::kind() const {
    return "Электросамокат";
}

std::string ElectricScooter::toString() const {
    std::ostringstream oss;
    oss << Vehicle::toString()
        << "\n  Макс. скорость: " << maxSpeedKmh_ << " км/ч"
        << "\n  Ёмкость батареи: " << batteryKWh_ << " кВт·ч"
        << "\n  Запас хода: " << rangeKm_ << " км";
    return oss.str();
}

double ElectricScooter::getMaxSpeedKmh() const { return maxSpeedKmh_; }
double ElectricScooter::getBatteryKWh() const { return batteryKWh_; }
double ElectricScooter::getRangeKm() const { return rangeKm_; }

void ElectricScooter::setMaxSpeedKmh(double maxSpeedKmh) {
    if (maxSpeedKmh <= 0)
        throw std::invalid_argument("Максимальная скорость должна быть больше нуля");
    maxSpeedKmh_ = maxSpeedKmh;
}

void ElectricScooter::setBatteryKWh(double batteryKWh) {
    if (batteryKWh <= 0)
        throw std::invalid_argument("Ёмкость батареи должна быть больше нуля");
    batteryKWh_ = batteryKWh;
}

void ElectricScooter::setRangeKm(double rangeKm) {
    if (rangeKm <= 0)
        throw std::invalid_argument("Запас хода должен быть больше нуля");
    rangeKm_ = rangeKm;
}
