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

    [HttpGet]
    public async Task<ActionResult<NamesListDto>> GetBlogPostsList()
    {
        var result = await _mediator.Send(new GetBlogPostsQuery());

        if (result.Success) return Ok(result.Data);
        
        return StatusCode(500, result.ErrorsList);
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
    
    [HttpGet("{link}")]
    public async Task<ActionResult<BlogPostDetailsDto>> GetBlogPost([FromRoute] string link)
    {
        var result = await _mediator.Send(new GetBlogPostQuery(link));
        if (result.Success) return Ok(result.Data);
        
        return StatusCode(500, result.ErrorsList);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<BlogPostDetailsDto>> DeleteBlogPost([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteBlogPostQuery(id));
        if (result.Success) return Ok(result.Data);
        
        return StatusCode(500, result.ErrorsList);
    }
    
}