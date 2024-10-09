using System.Linq.Expressions;
using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Shop;

public class GetOrdersInvoiceQuery : IRequest<OperationResult<FileDto>>
{
    public int Id { get; private set; }
    public GetOrdersInvoiceQuery(int id)
    {
        Id = id;
    }
}

public class GetOrdersInvoiceQueryHandler : IRequestHandler<GetOrdersInvoiceQuery, OperationResult<FileDto>>
{
    private readonly IPDRepository _repository;
    private readonly IEmailService _emailService;
    
    public GetOrdersInvoiceQueryHandler(IPDRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }
    
    public async Task<OperationResult<FileDto>> Handle(GetOrdersInvoiceQuery request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<FileDto>();

        Expression<Func<Order, object>> invoice = o => o.Invoice;
        Expression<Func<Order, object>> diets = o => o.Diets;
        
        var order = await _repository.FindEntityByConditionAsync<Order>(o => o.InvoiceId == request.Id,
            include: [invoice,diets]);

        var memoryStream = await _emailService.GeneratePdfFile(order);
        
        var invoiceDto = new FileDto()
        {
            FileName = $"Faktura numer {order.Invoice.InvoiceId}",
            Stream = memoryStream,
            MimeType = "application/pdf"
        };

        result.Data = invoiceDto;

        return result;
    }
}