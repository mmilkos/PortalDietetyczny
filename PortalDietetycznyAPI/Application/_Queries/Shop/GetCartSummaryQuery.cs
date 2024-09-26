using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Shop;

public class GetCartSummaryQuery : IRequest<OperationResult<CartSummaryResponse>>
{
    public CartSummaryRequest Dto { get; private set; }
    
    public GetCartSummaryQuery(CartSummaryRequest dto)
    {
        Dto = dto;
    }
}

public class GetCartSummaryQueryHandler : IRequestHandler<GetCartSummaryQuery, OperationResult<CartSummaryResponse>>
{
    private readonly IPDRepository _repository;
    
    public GetCartSummaryQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<CartSummaryResponse>> Handle(GetCartSummaryQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<CartSummaryResponse>()
        {
            Data = new CartSummaryResponse()
        };

        var selectedIds = request.Dto.ProductsIds;

        var selectedDiets = await _repository.FindEntitiesByConditionAsync<Domain.Entities.Diet>( 
            condition: diet => selectedIds.Contains(diet.Id),
            include: diet => diet.Photo);

        foreach (var diet in selectedDiets)
        {
            var cartProduct = new CartProduct()
            {
                Id = diet.Id,
                Name = diet.Name,
                PhotoUrl = diet.Photo.Url,
                Price = diet.Price
            };
            
            operationResult.Data.Products.Add(cartProduct);
        }

        return operationResult;
    }
}