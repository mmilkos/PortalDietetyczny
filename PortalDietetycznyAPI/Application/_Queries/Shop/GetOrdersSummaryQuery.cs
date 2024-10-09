using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Shop;

public class GetOrdersSummaryQuery : IRequest<PagedResult<OrderSummaryDto>>
{
    public OrdersSummaryRequestDto Dto { get; private set; }
    
    public GetOrdersSummaryQuery(OrdersSummaryRequestDto dto)
    {
        Dto = dto;
    }
}

public class GetOrdersSummaryQueryHandler : IRequestHandler<GetOrdersSummaryQuery, PagedResult<OrderSummaryDto>>
{
    private readonly IPDRepository _repository;
    
    public GetOrdersSummaryQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<PagedResult<OrderSummaryDto>> Handle(GetOrdersSummaryQuery request, CancellationToken cancellationToken)
    {
        var result = new PagedResult<OrderSummaryDto>()
        {
            Data = []
        };

        var ordersPaged = await _repository.GetOrdersPagedAsync(request.Dto);

        foreach (var order in ordersPaged)
        {
            var orderSummary = new OrderSummaryDto()
            {
                OrderId = order.OrderId,
                Amount = order.Amount,
                CustomerEmail = order.CustomerEmail,
                OrderStatus = order.OrderStatus,
                DietsNames = order.DietsNames,
                HasInvoice = order.HasInvoice,
                InvoiceId = order.InvoiceId
            };
            result.Data.Add(orderSummary);
        }

        return result;
    }
}