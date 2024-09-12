using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetBlogPostsQuery : IRequest<OperationResult<NamesListDto>>
{
    
}

public class GetBlogPostsQueryHandler : IRequestHandler<GetBlogPostsQuery, OperationResult<NamesListDto>>
{
    private readonly IPDRepository _repository;
    
    public GetBlogPostsQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<NamesListDto>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<NamesListDto>();

        var posts = await _repository.GetAllEntitiesAsync<BlogPost>(bp => true);
        
        operationResult.Data = new NamesListDto
        {
            Names = posts.Select(bp => new IdAndNameDto { Id = bp.Id, Name = bp.Title }).ToList()
        };

        return operationResult;
    }
}