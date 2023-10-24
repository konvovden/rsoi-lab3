using System.ComponentModel.DataAnnotations;
using CarsService.Api;
using GatewayService.Dto.Cars;
using GatewayService.Server.Dto.Converters.Cars;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GatewayService.Server.Controllers;

[ApiController]
[Route("/api/v1/cars")]
public class CarsController : ControllerBase
{
    private readonly CarsService.Api.CarsService.CarsServiceClient _carsServiceClient;
    
    public CarsController(CarsService.Api.CarsService.CarsServiceClient carsServiceClient)
    {
        _carsServiceClient = carsServiceClient;
    }

    /// <summary>
    /// Получить список всех доступных для бронирования автомобилей
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="showAll"></param>
    /// <response code="200">Список доступных для бронирования автомобилей</response>
    [HttpGet]
    [SwaggerOperation("ApiV1CarsGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(CarsList), description: "Список доступных для бронирования автомобилей")]
    public async Task<IActionResult> GetCars([FromQuery]int? page, [FromQuery][Range(1, 100)]int? size, [FromQuery]bool? showAll)
    {
        page ??= 1;
        size ??= 50;
        showAll ??= false;

        var response = await _carsServiceClient.GetCarsListAsync(new GetCarsListRequest()
        {
            Page = page.Value,
            Size = size.Value,
            ShowAll = showAll.Value
        });

        return Ok(CarsListConverter.Convert(response));
    }
}