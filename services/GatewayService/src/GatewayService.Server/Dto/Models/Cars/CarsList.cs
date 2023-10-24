using System.Runtime.Serialization;
using GatewayService.Server.Dto.Models.Cars;

namespace GatewayService.Dto.Cars;

[DataContract]
public class CarsList
{
    /// <summary>
    /// Номер страницы
    /// </summary>
    /// <value>Номер страницы</value>
    [DataMember(Name="page")]
    public int Page { get; set; }

    /// <summary>
    /// Количество элементов на странице
    /// </summary>
    /// <value>Количество элементов на странице</value>
    [DataMember(Name="pageSize")]
    public int PageSize { get; set; }

    /// <summary>
    /// Общее количество элементов
    /// </summary>
    /// <value>Общее количество элементов</value>
    [DataMember(Name="totalElements")]
    public int TotalElements { get; set; }

    /// <summary>
    /// Gets or Sets Items
    /// </summary>
    [DataMember(Name="items")]
    public List<Car> Items { get; set; }

    public CarsList(int page,
        int pageSize,
        int totalElements,
        List<Car> items)
    {
        Page = page;
        PageSize = pageSize;
        TotalElements = totalElements;
        Items = items;
    }
}