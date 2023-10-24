﻿using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace GatewayService.Dto.Payments.Enums;

/// <summary>
/// Статус платежа
/// </summary>
/// <value>Статус платежа</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum PaymentStatus
{
    /// <summary>
    /// Enum PAIDEnum for PAID
    /// </summary>
    [EnumMember(Value = "PAID")]
    Paid = 1,
            
    /// <summary>
    /// Enum REVERSEDEnum for REVERSED
    /// </summary>
    [EnumMember(Value = "REVERSED")]
    Reversed = 2
}