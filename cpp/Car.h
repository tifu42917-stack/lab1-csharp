#pragma once

#include "Vehicle.h"

class Car : public Vehicle {
public:
    static constexpr double kDefaultPrice = 1000000.0;
    static constexpr const char* kDefaultModel = "Unknown";
    static constexpr int kDefaultYear = 2020;
    static constexpr double kDefaultEngineVolumeL = 1.6;
    static constexpr int kMinYear = 1886;

    Car();
    Car(const std::string& brand, const std::string& color, double price,
        const std::string& model, int year, double engineVolumeL);

    std::string kind() const override;
    std::string toString() const override;

    const std::string& getModel() const;
    int getYear() const;
    double getEngineVolumeL() const;

    void setModel(const std::string& model);
    void setYear(int year);
    void setEngineVolumeL(double engineVolumeL);

private:
    std::string model_;
    int year_;
    double engineVolumeL_;
};
