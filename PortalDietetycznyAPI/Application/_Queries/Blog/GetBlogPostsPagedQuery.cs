using MediatR;
using PagedList;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetBlogPostsPagedQuery : IRequest<PagedResult<BlogPostPreviewDto>>
{
    public BlogPostsPreviewPageRequest Dto { get; private set; }
    
    public GetBlogPostsPagedQuery(BlogPostsPreviewPageRequest dto)
    {
        Dto = dto;
    }
    
}

public class GetBlogPostsPagedQueryHandler : IRequestHandler<GetBlogPostsPagedQuery, PagedResult<BlogPostPreviewDto>>
{
    private readonly IPDRepository _repository;
    
    public GetBlogPostsPagedQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<PagedResult<BlogPostPreviewDto>> Handle(GetBlogPostsPagedQuery request, CancellationToken cancellationToken)
    {
        var pagedResult = new PagedResult<BlogPostPreviewDto>()
        {
            Data = new List<BlogPostPreviewDto>()
        };
        
        var dto = request.Dto;

        IPagedList<BlogPost?> pagedBlogPosts;

        try
        {
            pagedBlogPosts = await _repository.GetBlogPostsPagedAsync(dto);
        }
        catch (Exception e)
        {
            pagedResult.Error = e.Message;
            return pagedResult;
        }

        pagedResult.Data = pagedBlogPosts.Select(bp => new BlogPostPreviewDto()
        {
            Id = bp.Id,
            Title = bp.Title,
            PhotoUrl = bp.Photo?.Url,
            Url = bp.Url
        }).ToList();
        
        pagedResult.PageNumber = pagedBlogPosts.PageNumber;
        pagedResult.TotalCount = pagedBlogPosts.TotalItemCount;

        return pagedResult;
    }
}