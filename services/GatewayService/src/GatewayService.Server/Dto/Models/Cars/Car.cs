using System.Runtime.Serialization;
using GatewayService.Dto.Cars.Enums;

namespace GatewayService.Server.Dto.Models.Cars;

[DataContract]
public class Car
{
    /// <summary>
    /// UUID автомобиля
    /// </summary>
    /// <value>UUID автомобиля</value>
    [DataMember(Name="carUid")]
    public string Id { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    /// <value>Марка автомобиля</value>
    [DataMember(Name="brand")]
    public string Brand { get; set; }

    /// <summary>
    /// Модель автомобиля
    /// </summary>
    /// <value>Модель автомобиля</value>
    [DataMember(Name="model")]
    public string Model { get; set; }

    /// <summary>
    /// Регистрационный номер автомобиля
    /// </summary>
    /// <value>Регистрационный номер автомобиля</value>
    [DataMember(Name="registrationNumber")]
    public string RegistrationNumber { get; set; }

    /// <summary>
    /// Мощность автомобиля в лошадиных силах
    /// </summary>
    /// <value>Мощность автомобиля в лошадиных силах</value>
    [DataMember(Name="power")]
    public int Power { get; set; }
    
    /// <summary>
    /// Тип автомобиля
    /// </summary>
    /// <value>Тип автомобиля</value>
    [DataMember(Name="type")]
    public CarType Type { get; set; }

    /// <summary>
    /// Цена автомобиля за сутки
    /// </summary>
    /// <value>Цена автомобиля за сутки</value>
    [DataMember(Name="price")]
    public int Price { get; set; }

    /// <summary>
    /// Флаг, указывающий что автомобиль доступен для бронирования
    /// </summary>
    /// <value>Флаг, указывающий что автомобиль доступен для бронирования</value>
    [DataMember(Name="available")]
    public bool Available { get; set; }

    public Car(string id,
        string brand,
        string model,
        string registrationNumber,
        int power,
        CarType type,
        int price,
        bool available)
    {
        Id = id;
        Brand = brand;
        Model = model;
        RegistrationNumber = registrationNumber;
        Power = power;
        Type = type;
        Price = price;
        Available = available;
    }
}