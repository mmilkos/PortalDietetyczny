using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Diet;

public class GetDietsQuery : IRequest<NamesListDto>
{
    
}

public class GetDietsQueryHandler : IRequestHandler<GetDietsQuery, NamesListDto>
{
    private readonly IPDRepository _repository;
    
    public GetDietsQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<NamesListDto> Handle(GetDietsQuery request, CancellationToken cancellationToken)
    {
        

        var diets = await _repository.GetAllEntitiesAsync<Domain.Entities.Diet>(d => true);
        
        var list  = new NamesListDto
        {
            Names = diets.Select(d => new IdAndNameDto { Id = d.Id, Name = d.Name }).ToList()
        };

        return list;
    }
}