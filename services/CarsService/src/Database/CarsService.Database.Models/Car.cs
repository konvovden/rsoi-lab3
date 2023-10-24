using CarsService.Database.Models.Enums;

namespace CarsService.Database.Models;

public class Car
{
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string RegistrationNumber { get; set; }
    public int Power { get; set; }
    public int Price { get; set; }
    public CarType Type { get; set; }
    public bool Availability { get; set; }

    public Car(Guid id, 
        string brand,
        string model,
        string registrationNumber,
        int power,
        int price,
        CarType type,
        bool availability)
    {
        Id = id;
        Brand = brand;
        Model = model;
        RegistrationNumber = registrationNumber;
        Power = power;
        Price = price;
        Type = type;
        Availability = availability;
    }
}