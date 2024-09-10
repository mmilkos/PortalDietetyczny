using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetBlogPostQuery : IRequest<OperationResult<BlogPostDetailsDto>>
{
    public int Uid { get; set; }
    
    public GetBlogPostQuery(int uid)
    {
        Uid = uid;
    }
}

public class GetBlogPostQueryHandler : IRequestHandler<GetBlogPostQuery, OperationResult<BlogPostDetailsDto>>
{
    private readonly IPDRepository _repository;
    
    public GetBlogPostQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<BlogPostDetailsDto>> Handle(GetBlogPostQuery request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<BlogPostDetailsDto>()
        {
            Data = new BlogPostDetailsDto()
        };
        
        var blogPost = await _repository.GetBlogPost(request.Uid);
        
        if (blogPost == null)
        {
            result.AddError(ErrorsRes.BlogPostNotFound);
            return result;
        }

        var blogPostDto = new BlogPostDetailsDto()
        {
            Uid = blogPost.Uid,
            Title = blogPost.Title,
            Content = blogPost.Content,
            PhotoUrl = blogPost.Photo?.Url,
            Url = blogPost.Url
        };

        result.Data = blogPostDto;

        return result;
    }
}