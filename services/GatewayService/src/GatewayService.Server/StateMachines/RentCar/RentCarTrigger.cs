namespace GatewayService.Server.StateMachines.RentCar;

public enum RentCarTrigger
{
    Started = 0,
    CarReserved = 1,
    PaymentCreated = 2,
    RentalCreated = 3,
    CarReservationCompensated = 4,
    PaymentCreationCompensated = 5,
    Fail = 6,
}