using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands.Shop;

public class StartOrderCommand : IRequest<OperationResult<Order>>
{
    public OrderDto Dto { get; private set; }
    public StartOrderCommand(OrderDto dto)
    {
        Dto = dto;
    }
    
}
public class StartOrderCommandHandler : IRequestHandler<StartOrderCommand, OperationResult<Order>>
{
    private readonly IPDRepository _repository;
    
    public StartOrderCommandHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<Order>> Handle(StartOrderCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Order>();

        var dto = request.Dto;
        
        var products = await _repository.FindEntitiesByConditionAsync<Diet>(p => dto.ProductsIds.Contains(p.Id));
        var id = await _repository.CountAsync<Order>();
        var date = DateTime.Now;
        var amount = products.Select(p => p.Price).Sum();

        Invoice? invoice = null;

        var hasInvoice = dto.InvoiceDto != null;
        
        if (hasInvoice)
        {
            invoice = new Invoice()
            {
                IssueDate = date,
                SaleDate = date,
                Buyer = new InvoiceParty()
                {
                    Name = dto.InvoiceDto.Name,
                    LastName = dto.InvoiceDto.LastName,
                    Street = dto.InvoiceDto.Street,
                    City = dto.InvoiceDto.City
                },
                Seller = new InvoiceParty()
                {
                    Name = "Agnieszka",
                    LastName = "Miłkowska",
                    Street = "Dziewanny 17a",
                    City = "05-077 WARSZAWA - WESOŁA"
                },
                Diets = products,
                Amount = amount
            };
        }

        var order = new Order()
        {
            OrderId = $"{id}-{date.ToString("dd-MM-yyyy").Replace("-", "")}",
            Amount = products.Select(p => p.Price).Sum(),
            CustomerEmail = dto.CustomerEmail,
            OrderStatus = OrderStatus.CREATED,
            OrderedDiets = products,
            DietsNames = string.Join(";",products.Select(p =>p.Name)),
            HasInvoice = hasInvoice,
            InvoiceId = invoice?.Id,
            Invoice = invoice,
        };

        try
        {
            await _repository.AddAsync(order);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.OrderSavingError);
            return operationResult;
        }

        operationResult.Data = order;

        return operationResult;
    }
}
