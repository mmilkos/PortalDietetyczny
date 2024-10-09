using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Application._Queries;
using PortalDietetycznyAPI.Application._Queries.Diet;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/diets")]
public class DietController : ControllerBase
{
    
    private IMediator _mediator;

    public DietController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> AddDiet([FromBody] AddDietDto dto)
    {
        
        var result = await _mediator.Send(new AddDietCommand(dto));
        if (result.Success) return Ok();
        
        return StatusCode(500, result.ErrorsList);
    } 
    
    [HttpGet("tags")]
    public async Task<ActionResult<NamesListDto>> GetTags()
    {
        var result = await _mediator.Send(new GetTagsQuery(TagContext.Diet));
        
        if (result.Success) return Ok(result.Data);
        
        return StatusCode(500, result.ErrorsList);
    }
    
    [Authorize]
    [HttpPost("tags")]
    public async Task<ActionResult<NamesListDto>> AddTags([FromBody] AddTagDto dto)
    {
        var result = await _mediator.Send(new AddTagCommand(dto));
        if (result.Success) return Ok();
        
        return StatusCode(500, result.ErrorsList);
    }
    
    [Authorize]
    [HttpDelete("tags/{id}")]
    public async Task<ActionResult> DeleteTag(int id)
    {
        var result = await _mediator.Send(new DeleteTagCommand(id));

        if (result.Success) return Ok();

        return StatusCode(500, result.ErrorsList);
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDiet(int id)
    {
        var result = await _mediator.Send(new DeleteDietCommand(id));

        if (result.Success) return Ok();

        return StatusCode(500, result.ErrorsList);
    }
    
    [HttpPost("paged")]
    public async Task<ActionResult<PagedResult<DietPreviewDto>>> GetDietsPaged([FromBody] DietsPreviewPageRequest dto)
    {
        var result = await _mediator.Send(new GetDietsPagedQuery(dto));

        return Ok(result);
    }
    
    [HttpGet()]
    public async Task<ActionResult<PagedResult<DietPreviewDto>>> GetDiets()
    {
        var result = await _mediator.Send(new GetDietsQuery());

        return Ok(result);
    }
}

