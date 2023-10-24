using System.ComponentModel.DataAnnotations;
using CarsService.Api;
using GatewayService.Server.Dto.Converters;
using GatewayService.Server.Dto.Converters.Rental;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Api;
using RentalService.Api;
using Swashbuckle.AspNetCore.Annotations;
using CreateRentalRequest = GatewayService.Server.Dto.Models.Rental.CreateRentalRequest;
using Rental = GatewayService.Server.Dto.Models.Rental.Rental;

namespace GatewayService.Server.Controllers;

[ApiController]
[Route("/api/v1/rental")]
public class RentalController : ControllerBase
{
    private readonly RentalService.Api.RentalService.RentalServiceClient _rentalServiceClient;
    private readonly CarsService.Api.CarsService.CarsServiceClient _carsServiceClient;
    private readonly PaymentService.Api.PaymentService.PaymentServiceClient _paymentServiceClient;
    
    public RentalController(RentalService.Api.RentalService.RentalServiceClient rentalServiceClient, 
        CarsService.Api.CarsService.CarsServiceClient carsServiceClient, 
        PaymentService.Api.PaymentService.PaymentServiceClient paymentServiceClient)
    {
        _rentalServiceClient = rentalServiceClient;
        _carsServiceClient = carsServiceClient;
        _paymentServiceClient = paymentServiceClient;
    }

    /// <summary>
    /// Получить информацию о всех арендах пользователя
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <response code="200">Информация обо всех арендах</response>
    [HttpGet]
    [SwaggerOperation("ApiV1RentalGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<Rental>), description: "Информация обо всех арендах")]
    public async Task<IActionResult> ApiV1RentalGet([FromHeader(Name = "X-User-Name")][Required]string username)
    {
        var rentals = await _rentalServiceClient.GetUserRentalsAsync(new GetUserRentalsRequest()
        {
            Username = username
        });

        var carIds = rentals.Rentals
            .Select(r => r.CarId)
            .ToList();

        var getCarsResponse = await _carsServiceClient.GetCarsAsync(new GetCarsRequest()
        {
            Ids = { carIds }
        });
        
        var paymentIds = rentals.Rentals
            .Select(r => r.PaymentId)
            .ToList();

        var getPaymentsResponse = await _paymentServiceClient.GetPaymentsAsync(new GetPaymentsRequest()
        {
            Ids = { paymentIds }
        });

        var rentalsDto = rentals.Rentals
            .Select(r => RentalConverter.Convert(r,
                getCarsResponse.Cars.First(c => c.Id == r.CarId),
                getPaymentsResponse.Payments.First(p => p.Id == r.PaymentId)));

        return Ok(rentalsDto);
    }

    /// <summary>
    /// Забронировать автомобиль
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="createRentalRequest"></param>
    /// <response code="200">Информация о бронировании авто</response>
    /// <response code="400">Ошибка валидации данных</response>
    [HttpPost]
    [SwaggerOperation("ApiV1RentalPost")]
    [SwaggerResponse(statusCode: 200, type: typeof(Rental), description: "Информация о бронировании авто")]
    [SwaggerResponse(statusCode: 400, description: "Ошибка валидации данных")]
    public async Task<IActionResult> ApiV1RentalPost([FromHeader(Name = "X-User-Name")][Required]string username, 
        [FromBody]CreateRentalRequest createRentalRequest)
    {
        var reserveCarResponse = await _carsServiceClient.ReserveCarAsync(new ReserveCarRequest()
        {
            Id = createRentalRequest.CarId
        });

        var days = createRentalRequest.DateTo.DayNumber - createRentalRequest.DateFrom.DayNumber;
        var totalPrice = reserveCarResponse.Car.Price * days;

        var createPaymentResponse = await _paymentServiceClient.CreatePaymentAsync(new CreatePaymentRequest()
        {
            Price = totalPrice
        });

        var createRentalResponse = await _rentalServiceClient.CreateRentalAsync(new RentalService.Api.CreateRentalRequest() 
        {
            Username = username,
            CarId = createRentalRequest.CarId,
            PaymentId = createPaymentResponse.Payment.Id,
            DateFrom = DateConverter.Convert(createRentalRequest.DateFrom),
            DateTo = DateConverter.Convert(createRentalRequest.DateTo)
        });

        return Ok(RentalConverter.Convert(createRentalResponse.Rental,
            reserveCarResponse.Car,
            createPaymentResponse.Payment));
    }

    /// <summary>
    /// Отмена аренды автомобиля
    /// </summary>
    /// <param name="rentalId">UUID аренды</param>
    /// <param name="username">Имя пользователя</param>
    /// <response code="204">Аренда успешно отменена</response>
    /// <response code="404">Аренда не найдена</response>
    [HttpDelete("{rentalId}")]
    [SwaggerOperation("ApiV1RentalRentalUidDelete")]
    [SwaggerResponse(statusCode: 404, description: "Аренда не найдена")]
    public async Task<IActionResult> ApiV1RentalRentalUidDelete([FromRoute][Required] string rentalId,
        [FromHeader(Name = "X-User-Name")][Required] string username)
    {
        var cancelRentalResponse = await _rentalServiceClient.CancelRentalAsync(new CancelRentalRequest()
        {
            Username = username,
            RentalId = rentalId
        });

        await _paymentServiceClient.CancelPaymentAsync(new CancelPaymentRequest()
        {
            Id = cancelRentalResponse.Rental.PaymentId
        });

        await _carsServiceClient.RemoveReserveFromCarAsync(new RemoveReserveFromCarRequest()
        {
            Id = cancelRentalResponse.Rental.CarId
        });

        return NoContent();
    }

    /// <summary>
    /// Завершение аренды автомобиля
    /// </summary>
    /// <param name="rentalId">UUID аренды</param>
    /// <param name="username">Имя пользователя</param>
    /// <response code="204">Аренда успешно завершена</response>
    /// <response code="404">Аренда не найдена</response>
    [HttpPost("{rentalId}/finish")]
    [SwaggerOperation("ApiV1RentalRentalUidFinishPost")]
    [SwaggerResponse(statusCode: 404, description: "Аренда не найдена")]
    public async Task<IActionResult> ApiV1RentalRentalUidFinishPost([FromRoute][Required]string rentalId,
        [FromHeader(Name = "X-User-Name")][Required]string username)
    {
        var finishRentalResponse = await _rentalServiceClient.FinishRentalAsync(new FinishRentalRequest()
        {
            Username = username,
            RentalId = rentalId
        });

        await _carsServiceClient.RemoveReserveFromCarAsync(new RemoveReserveFromCarRequest()
        {
            Id = finishRentalResponse.Rental.CarId
        });

        return NoContent();
    }

    /// <summary>
    /// Информация по конкретной аренде пользователя
    /// </summary>
    /// <param name="rentalId">UUID аренды</param>
    /// <param name="username">Имя пользователя</param>
    /// <response code="200">Информация по конкретному бронированию</response>
    /// <response code="404">Билет не найден</response>
    [HttpGet("{rentalId}")]
    [SwaggerOperation("ApiV1RentalRentalUidGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Rental), description: "Информация по конкретному бронированию")]
    [SwaggerResponse(statusCode: 404, description: "Билет не найден")]
    public async Task<IActionResult> ApiV1RentalRentalUidGet([FromRoute][Required]string rentalId, 
        [FromHeader(Name = "X-User-Name")][Required]string username)
    {
        var getUserRentalResponse = await _rentalServiceClient.GetUserRentalAsync(new GetUserRentalRequest()
        {
            RentalId = rentalId,
            Username = username
        });

        var getCarResponse = await _carsServiceClient.GetCarAsync(new GetCarRequest()
        {
            Id = getUserRentalResponse.Rental.CarId
        });
        
        var getPaymentResponse = await _paymentServiceClient.GetPaymentAsync(new GetPaymentRequest()
        {
            Id = getUserRentalResponse.Rental.PaymentId
        });

        return Ok(RentalConverter.Convert(getUserRentalResponse.Rental, getCarResponse.Car, getPaymentResponse.Payment));
    }
}