using System.Runtime.Serialization;
using GatewayService.Server.Dto.Models.Payments.Enums;

namespace GatewayService.Dto.Payments;

[DataContract]
public class Payment
{
    /// <summary>
    /// UUID платежа
    /// </summary>
    /// <value>UUID платежа</value>
    [DataMember(Name="paymentUid")]
    public string Id { get; set; }

    /// <summary>
    /// Статус платежа
    /// </summary>
    /// <value>Статус платежа</value>
    [DataMember(Name="status")]
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// Сумма платежа
    /// </summary>
    /// <value>Сумма платежа</value>
    [DataMember(Name="price")]
    public int Price { get; set; }

    public Payment(string id, 
        PaymentStatus status,
        int price)
    {
        Id = id;
        Status = status;
        Price = price;
    }
}