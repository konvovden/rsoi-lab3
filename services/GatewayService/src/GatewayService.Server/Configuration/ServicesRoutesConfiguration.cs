namespace GatewayService.Server.Configuration;

public class ServicesRoutesConfiguration
{
    public string CarsServiceUri { get; set; }
    public string PaymentServiceUri { get; set; }
    public string RentalServiceUri { get; set; }

    public ServicesRoutesConfiguration(string carsServiceUri, 
        string paymentServiceUri,
        string rentalServiceUri)
    {
        CarsServiceUri = carsServiceUri;
        PaymentServiceUri = paymentServiceUri;
        RentalServiceUri = rentalServiceUri;
    }
}