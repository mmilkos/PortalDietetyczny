using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetIngredientsQuery : IRequest<OperationResult<IngredientListDto>>
{
    
}

public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, OperationResult<IngredientListDto>>
{
    private readonly IPDRepository _repository;
    
    public GetIngredientsQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<IngredientListDto>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<IngredientListDto>()
        {
            Data = new IngredientListDto()
        };

        var ingredients = await _repository.GetAllEntitiesAsync<Ingredient>(i=> true);

        foreach (var ingredient in ingredients)
        {
            var dto = new IdAndNameDto()
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
            
            operationResult.Data.Ingredients.Add(dto);
        }

        return operationResult;
    }
}