using System.Net;
using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetBlogPostQuery : IRequest<OperationResult<BlogPostDetailsDto>>
{
    public string Url { get; private set; }
    
    public GetBlogPostQuery(string url)
    {
        Url = url;
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
        
        var uid = int.Parse(request.Url.Split('-').Last(), System.Globalization.NumberStyles.HexNumber);
        
        var blogPost = await _repository.GetBlogPost(uid);
        
        if (blogPost == null || blogPost.Url != request.Url)
        {
            result.SetStatusCode(HttpStatusCode.NotFound);
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