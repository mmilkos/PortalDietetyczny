using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;
using PortalDietetycznyAPI.Infrastructure.Repositories;
using static PortalDietetycznyAPI.Domain.Common.TagContext;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetTagsQuery : IRequest<OperationResult<NamesListDto>>
{
    public TagContext TagContext { get; private set; }
    public GetTagsQuery(TagContext tagContext)
    {
        TagContext = tagContext;
    }
}

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, OperationResult<NamesListDto>>
{
    private readonly IPDRepository _repository;

    public GetTagsQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<NamesListDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<NamesListDto>()
        {
            Data = new NamesListDto()
            {
                Names = new List<IdAndNameDto>()
            }
        };

        var xd = request.TagContext;

        List<Tag> tags;

        if (request.TagContext == TagContext.Diet)
        {
            tags = await _repository.GetAllEntitiesAsync<Tag>(tag => tag.Context == TagContext.Diet);
        }
        else
        {
            tags = await _repository.GetAllEntitiesAsync<Tag>(tag => tag.Context == TagContext.Recipe);
        }

        foreach (var tag in tags)
        {
            var dto = new IdAndNameDto()
            {
                Id = tag.Id,
                Name = tag.Name
            };
            
            operationResult.Data.Names.Add(dto);
        }

        return operationResult;
    }
}