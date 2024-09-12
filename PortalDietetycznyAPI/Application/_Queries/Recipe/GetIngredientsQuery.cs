using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetIngredientsQuery : IRequest<OperationResult<NamesListDto>>
{
    
}

public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, OperationResult<NamesListDto>>
{
    private readonly IPDRepository _repository;
    
    public GetIngredientsQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<NamesListDto>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<NamesListDto>()
        {
            Data = new NamesListDto()
        };

        var ingredients = await _repository.GetAllEntitiesAsync<Ingredient>(i=> true);

        foreach (var ingredient in ingredients)
        {
            var dto = new IdAndNameDto()
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
            
            operationResult.Data.Names.Add(dto);
        }

        return operationResult;
    }
}