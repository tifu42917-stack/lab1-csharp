#pragma once

#include <ostream>
#include <string>

class Vehicle {
public:
    static constexpr const char* kDefaultBrand = "Unknown";
    static constexpr const char* kDefaultColor = "White";
    static constexpr double kDefaultPrice = 100000.0;

    Vehicle();
    Vehicle(const std::string& brand, const std::string& color, double price);
    virtual ~Vehicle() = default;

    virtual std::string kind() const;
    virtual std::string toString() const;

    const std::string& getBrand() const;
    const std::string& getColor() const;
    double getPrice() const;

    void setBrand(const std::string& brand);
    void setColor(const std::string& color);
    void setPrice(double price);

private:
    std::string brand_;
    std::string color_;
    double price_;
};

std::ostream& operator<<(std::ostream& out, const Vehicle& vehicle);
