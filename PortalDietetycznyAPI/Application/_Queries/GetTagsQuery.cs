using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;
using PortalDietetycznyAPI.Infrastructure.Repositories;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetTagsQuery : IRequest<OperationResult<TagListDto>>
{
    
}

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, OperationResult<TagListDto>>
{
    private readonly IPDRepository _repository;

    public GetTagsQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<TagListDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<TagListDto>()
        {
            Data = new TagListDto()
            {
                Tags = new List<IdAndNameDto>()
            }
        };

        var tags = await _repository.GetAllEntitiesAsync<Tag>(t => true);

        foreach (var tag in tags)
        {
            var dto = new IdAndNameDto()
            {
                Id = tag.Id,
                Name = tag.Name
            };
            
            operationResult.Data.Tags.Add(dto);
        }

        return operationResult;
    }
}