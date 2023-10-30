using System.Runtime.Serialization;
using GatewayService.Dto.Payments;
using GatewayService.Dto.Rental.Enums;
using GatewayService.Server.Dto.Models.Cars;

namespace GatewayService.Server.Dto.Models.Rental;

[DataContract]
public class Rental
{
    /// <summary>
    /// UUID аренды
    /// </summary>
    /// <value>UUID аренды</value>
    [DataMember(Name="rentalUid")]
    public string Id { get; set; }

    /// <summary>
    /// Статус аренды
    /// </summary>
    /// <value>Статус аренды</value>
    [DataMember(Name="status")]
    public RentalStatus Status { get; set; }

    /// <summary>
    /// Дата начала аренды
    /// </summary>
    /// <value>Дата начала аренды</value>
    [DataMember(Name="dateFrom")]
    public DateOnly DateFrom { get; set; }

    /// <summary>
    /// Дата окончания аренды
    /// </summary>
    /// <value>Дата окончания аренды</value>
    [DataMember(Name="dateTo")]
    public DateOnly DateTo { get; set; }

    /// <summary>
    /// UUID автомобиля
    /// </summary>
    /// <value>UUID автомобиля</value>
    [DataMember(Name = "carUid")]
    public string CarId { get; set; }

    /// <summary>
    /// Gets or Sets Car
    /// </summary>
    [DataMember(Name="car")]
    public Car? Car { get; set; }

    /// <summary>
    /// UUID платежа
    /// </summary>
    /// <value>UUID платежа</value>
    [DataMember(Name = "paymentUid")]
    public string PaymentId { get; set; }
    
    /// <summary>
    /// Gets or Sets Payment
    /// </summary>
    [DataMember(Name="payment", EmitDefaultValue = false)]
    public Payment? Payment { get; set; }

    public Rental(string id, 
        RentalStatus status,
        DateOnly dateFrom,
        DateOnly dateTo,
        string carId,
        Car? car,
        string paymentId,
        Payment? payment)
    {
        Id = id;
        Status = status;
        DateFrom = dateFrom;
        DateTo = dateTo;
        CarId = carId;
        Car = car;
        PaymentId = paymentId;
        Payment = payment;
    }
}