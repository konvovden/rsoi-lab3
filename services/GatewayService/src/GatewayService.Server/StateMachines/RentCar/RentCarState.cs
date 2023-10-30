namespace GatewayService.Server.StateMachines.RentCar;

public enum RentCarState
{
    Initial = 0,
    ReservingCar = 1,
    CreatingPayment = 2,
    CreatingRental = 3,
    Completed = 4,
    ReservingCarCompensation = 5,
    CreatingPaymentCompensation = 6,
    Failed = 7
}