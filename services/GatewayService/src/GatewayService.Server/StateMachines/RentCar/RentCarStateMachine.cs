using CarsService.Api;
using GatewayService.Server.Dto.Converters;
using GatewayService.Server.Dto.Converters.Rental;
using Grpc.Core;
using PaymentService.Api;
using RentalService.Api;
using Stateless;
using State = GatewayService.Server.StateMachines.RentCar.RentCarState;
using Trigger = GatewayService.Server.StateMachines.RentCar.RentCarTrigger;

namespace GatewayService.Server.StateMachines.RentCar;

public class RentCarStateMachine
{
    private string? _username;
    private string? _carId;
    private DateOnly? _dateFrom;
    private DateOnly? _dateTo;

    private Car? _car;
    private Payment? _payment;
    private Rental? _rental;
    private RpcException? _failureException;

    private readonly StateMachine<State, Trigger> _stateMachine;
    
    private readonly CarsService.Api.CarsService.CarsServiceClient _carsServiceClient;
    private readonly PaymentService.Api.PaymentService.PaymentServiceClient _paymentServiceClient;
    private readonly RentalService.Api.RentalService.RentalServiceClient _rentalServiceClient;
    
    public RentCarStateMachine(CarsService.Api.CarsService.CarsServiceClient carsServiceClient, 
        PaymentService.Api.PaymentService.PaymentServiceClient paymentServiceClient, 
        RentalService.Api.RentalService.RentalServiceClient rentalServiceClient)
    {
        _carsServiceClient = carsServiceClient;
        _paymentServiceClient = paymentServiceClient;
        _rentalServiceClient = rentalServiceClient;
        
        _stateMachine = new StateMachine<State, Trigger>(State.Initial);
        
        ConfigureStateMachine();
    }

    private void ConfigureStateMachine()
    {
        _stateMachine.Configure(State.Initial)
            .Permit(Trigger.Started, State.ReservingCar);

        _stateMachine.Configure(State.ReservingCar)
            .OnEntryAsync(async () =>
            {
                try
                {
                    var response = await _carsServiceClient.ReserveCarAsync(new ReserveCarRequest()
                    {
                        Id = _carId
                    });

                    _car = response.Car;
                    
                    await _stateMachine.FireAsync(Trigger.CarReserved);
                }
                catch (RpcException e)
                {
                    _failureException = e;
                    
                    await _stateMachine.FireAsync(Trigger.Fail);
                }
            })
            .Permit(Trigger.CarReserved, State.CreatingPayment)
            .Permit(Trigger.Fail, State.Failed);

        _stateMachine.Configure(State.CreatingPayment)
            .OnEntryAsync(async () =>
            {
                try
                {
                    var response = await _paymentServiceClient.CreatePaymentAsync(new CreatePaymentRequest()
                    {
                        Price = GetRentalTotalPrice()
                    });

                    _payment = response.Payment;
                    
                    await _stateMachine.FireAsync(Trigger.PaymentCreated);
                }
                catch (RpcException e)
                {
                    _failureException = e;
                    
                    await _stateMachine.FireAsync(Trigger.Fail);
                }
            })
            .Permit(Trigger.PaymentCreated, State.CreatingRental)
            .Permit(Trigger.Fail, State.ReservingCarCompensation);

        _stateMachine.Configure(State.CreatingRental)
            .OnEntryAsync(async () =>
            {
                try
                {
                    var response = await _rentalServiceClient.CreateRentalAsync(new CreateRentalRequest() 
                    {
                        Username = _username,
                        CarId = _carId,
                        PaymentId = _payment!.Id,
                        DateFrom = DateConverter.Convert(_dateFrom!.Value),
                        DateTo = DateConverter.Convert(_dateTo!.Value)
                    });

                    _rental = response.Rental;
                    
                    await _stateMachine.FireAsync(Trigger.RentalCreated);
                }
                catch (RpcException e)
                {
                    _failureException = e;
                    
                    await _stateMachine.FireAsync(Trigger.Fail);
                }
            })
            .Permit(Trigger.RentalCreated, State.Completed)
            .Permit(Trigger.Fail, State.CreatingPaymentCompensation);

        _stateMachine.Configure(State.ReservingCarCompensation)
            .OnEntryAsync(async () =>
            {
                try
                {
                    await _carsServiceClient.RemoveReserveFromCarAsync(new RemoveReserveFromCarRequest()
                    {
                        Id = _carId
                    });
                }
                catch (Exception)
                {
                    // pass
                }
                
                await _stateMachine.FireAsync(Trigger.CarReservationCompensated);
            })
            .Permit(Trigger.CarReservationCompensated, State.Failed);

        _stateMachine.Configure(State.CreatingPaymentCompensation)
            .OnEntryAsync(async () =>
            {
                try
                {
                    await _paymentServiceClient.CancelPaymentAsync(new CancelPaymentRequest()
                    {
                        Id = _payment!.Id
                    });
                }
                catch (Exception)
                {
                    // pass
                }

                await _stateMachine.FireAsync(Trigger.PaymentCreationCompensated);
            })
            .Permit(Trigger.PaymentCreationCompensated, State.ReservingCarCompensation);
    }

    public async Task<Dto.Models.Rental.Rental> StartAsync(string username, string carId, DateOnly dateFrom, DateOnly dateTo)
    {
        _username = username;
        _carId = carId;
        _dateFrom = dateFrom;
        _dateTo = dateTo;

        await _stateMachine.FireAsync(Trigger.Started);

        if (_stateMachine.IsInState(State.Completed))
            return RentalConverter.Convert(_rental!, _car, _payment);

        throw _failureException!;
    }

    private int GetRentalTotalPrice()
    {
        var days = _dateTo!.Value.DayNumber - _dateFrom!.Value.DayNumber;
        return _car!.Price * days;
    }
}