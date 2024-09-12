using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetRecipesQuery : IRequest<OperationResult<NamesListDto>>
{
    
}

public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, OperationResult<NamesListDto>>
{
    private readonly IPDRepository _repository;
    
    public GetRecipesQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<NamesListDto>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<NamesListDto>();

        var posts = await _repository.GetAllEntitiesAsync<Recipe>(bp => true);
        
        operationResult.Data = new NamesListDto
        {
            Names = posts.Select(recipe => new IdAndNameDto { Id = recipe.Id, Name = recipe.Name }).ToList()
        };

        return operationResult;
    }
}