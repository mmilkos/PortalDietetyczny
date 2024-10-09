using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;

namespace PortalDietetycznyAPI.Application._Commands.Shop;

public class ChangeOrderStatusCommand :  IRequest<OperationResult<Unit>>
{
    public string OrderId { get; private set; }
    public string NewStatus { get; private set; }
    
    public ChangeOrderStatusCommand(string orderId, string newStatus)
    {
        OrderId = orderId;
        NewStatus = newStatus;
    }
}

public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, OperationResult<Unit>>
{
    private readonly IPDRepository _repository;
    
    public ChangeOrderStatusCommandHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<Unit>> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>() {};
        
        var orderId = int.Parse(request.OrderId.Split('-')[0]);

        var order = await _repository.FindEntityByConditionAsync<Order>(o => o.Id == orderId);

        order.OrderStatus = request.NewStatus;

        try
        {
            await _repository.UpdateAsync(order);

        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.OrderSavingError);
        }

        return operationResult;
    }
}