using System.Runtime.Serialization;

namespace GatewayService.Server.Dto.Models.Rental;

[DataContract]
public class CreateRentalRequest
{
    /// <summary>
    /// UUID автомобиля
    /// </summary>
    /// <value>UUID автомобиля</value>
    [DataMember(Name="carUid")]
    public string CarId { get; set; }

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

    public CreateRentalRequest(string carId,
        DateOnly dateFrom, 
        DateOnly dateTo)
    {
        CarId = carId;
        DateFrom = dateFrom;
        DateTo = dateTo;
    }
}