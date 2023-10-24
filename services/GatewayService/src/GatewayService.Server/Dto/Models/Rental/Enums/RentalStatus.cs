using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GatewayService.Dto.Rental.Enums;

/// <summary>
/// Статус аренды
/// </summary>
/// <value>Статус аренды</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum RentalStatus
{
    [EnumMember(Value = "NEW")]
    New = 0,
    
    [EnumMember(Value = "IN_PROGRESS")]
    InProgress = 1,
            
    [EnumMember(Value = "FINISHED")]
    Finished = 2,
            
    [EnumMember(Value = "CANCELED")]
    Canceled = 3
}