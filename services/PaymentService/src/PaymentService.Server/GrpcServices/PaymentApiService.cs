using Grpc.Core;
using PaymentService.Api;
using PaymentService.Core.Services;
using PaymentService.Server.Converters;

namespace PaymentService.Server.GrpcServices;

public class PaymentApiService : Api.PaymentService.PaymentServiceBase
{
    private readonly IPaymentService _paymentService;

    public PaymentApiService(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public override async Task<GetPaymentResponse> GetPayment(GetPaymentRequest request, ServerCallContext context)
    {
        var paymentId = Guid.Parse(request.Id);

        var payment = await _paymentService.GetPaymentAsync(paymentId);

        return new GetPaymentResponse()
        {
            Payment = PaymentConverter.Convert(payment)
        };
    }

    public override async Task<GetPaymentsResponse> GetPayments(GetPaymentsRequest request, ServerCallContext context)
    {
        var paymentIds = request.Ids
            .Select(Guid.Parse)
            .ToList();

        var payments = await _paymentService.GetPaymentsAsync(paymentIds);

        var notFoundPayments = paymentIds
            .Except(payments.Select(p => p.Id))
            .Select(id => id.ToString());

        return new GetPaymentsResponse()
        {
            Payments = {payments.ConvertAll(PaymentConverter.Convert)},
            NotFoundIds = {notFoundPayments}
        };
    }

    public override async Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request, ServerCallContext context)
    {
        var payment = await _paymentService.CreatePaymentAsync(Guid.NewGuid(), request.Price);

        return new CreatePaymentResponse()
        {
            Payment = PaymentConverter.Convert(payment)
        };
    }

    public override async Task<CancelPaymentResponse> CancelPayment(CancelPaymentRequest request, ServerCallContext context)
    {
        var paymentId = Guid.Parse(request.Id);

        var payment = await _paymentService.CancelPaymentAsync(paymentId);

        return new CancelPaymentResponse()
        {
            Payment = PaymentConverter.Convert(payment)
        };
    }
}