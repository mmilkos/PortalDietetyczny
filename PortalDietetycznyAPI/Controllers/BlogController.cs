using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Application._Queries;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/blog")]
public class BlogController : ControllerBase
{
    private IMediator _mediator;

    public BlogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> PostPost([FromBody] AddBlogPostDto dto)
    {
        var result = await _mediator.Send(new AddBlogPostCommand(dto));

        if (result.Success) return Ok();

        return StatusCode(500, result.ErrorsList);
    }
    
    [HttpPost("paged")]
    public async Task<ActionResult<PagedResult<BlogPostPreviewDto>>> GetPostsPaged([FromBody] BlogPostsPreviewPageRequest dto)
    {
        var result = await _mediator.Send(new GetBlogPostsPagedQuery(dto));

        if (result.Error.IsNullOrEmpty()) return Ok(result);

        return StatusCode(500, result.Error);
    }
    
    [HttpGet("{uid}")]
    public async Task<ActionResult<BlogPostDetailsDto>> GetBlogPost([FromRoute] int uid)
    {
        var result = await _mediator.Send(new GetBlogPostQuery(uid));
        if (result.Success) return Ok(result.Data);
        
        return StatusCode(500, result.ErrorsList);
    }
}