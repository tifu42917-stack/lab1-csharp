#pragma once

#include "Vehicle.h"

class ElectricScooter : public Vehicle {
public:
    static constexpr const char* kDefaultColor = "Black";
    static constexpr double kDefaultPrice = 30000.0;
    static constexpr double kDefaultMaxSpeedKmh = 25.0;
    static constexpr double kDefaultBatteryKWh = 0.5;
    static constexpr double kDefaultRangeKm = 30.0;

    ElectricScooter();
    ElectricScooter(const std::string& brand, const std::string& color, double price,
                    double maxSpeedKmh, double batteryKWh, double rangeKm);

    std::string kind() const override;
    std::string toString() const override;

    double getMaxSpeedKmh() const;
    double getBatteryKWh() const;
    double getRangeKm() const;

    void setMaxSpeedKmh(double maxSpeedKmh);
    void setBatteryKWh(double batteryKWh);
    void setRangeKm(double rangeKm);

private:
    double maxSpeedKmh_;
    double batteryKWh_;
    double rangeKm_;
};
