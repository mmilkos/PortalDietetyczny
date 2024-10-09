using System.Security.Cryptography;
using System.Text;
using Flurl.Http;
using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands.Shop;

public class SendPaymentRequestCommand : IRequest<OperationResult<Unit>>
{
    public Order Order { get; private set; }

    public SendPaymentRequestCommand(Order order)
    {
        Order = order;
    }
}

public class SendPaymentRequestCommandHandler : IRequestHandler<SendPaymentRequestCommand, OperationResult<Unit>>
{
    private readonly IKeyService _keyService;
    private readonly string _apiUrl;
    
    public SendPaymentRequestCommandHandler(IKeyService keyService)
    {
        _keyService = keyService;
        _apiUrl = "https://testpay.autopay.eu";
    }
    
    public async Task<OperationResult<Unit>> Handle(SendPaymentRequestCommand request, CancellationToken cancellationToken)
    {
        
        var operationResult = new OperationResult<Unit>();

        var order = request.Order;

        var autopaySettings =  await _keyService.GetAutopaySettings();

        var validityTime = DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss");
        
        var paymentRequest = new PaymentRequest()
        {
            ServiceID = autopaySettings.ServiceID,
            OrderID = order.OrderId,
            Amount = order.Amount,
            CustomerEmail = order.CustomerEmail,
            ValidityTime = validityTime,
            LinkValidityTime = validityTime,
            Hash = GenerateHash(order: order, settings: autopaySettings, validityTime: validityTime)
        };

        //var postResult = await _apiUrl.PostJsonAsync(paymentRequest);

        return operationResult;
    }

    private string GenerateHash(Order order, AutopaySettings settings, string validityTime)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(
            $"Amount={order.Amount}CustomerEmail={order.CustomerEmail}LinkValidityTime={validityTime}OrderID={order.OrderId}ServiceID={settings.ServiceID}ValidityTime={validityTime}");

        stringBuilder.Append(settings.ApiKey);

        var bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());

        var hashBytes = SHA256.HashData(bytes);

        var hexHash = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();

        return hexHash;
    }
}