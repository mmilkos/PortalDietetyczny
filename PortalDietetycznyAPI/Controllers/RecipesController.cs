using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Application._Queries;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/recipes")]
public class RecipesController : ControllerBase
{
    private IMediator _mediator;

    public RecipesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{uid}")]
    public async Task<ActionResult<RecipeDetailsDto>> GetRecipe([FromRoute] int uid)
    {
       
        var result = await _mediator.Send(new GetRecipeQuery(uid));
        if (result.Success) return Ok(result.Data);
        
        return StatusCode(500, result.ErrorsList);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddRecipe([FromBody] AddRecipeDto dto)
    {
        var result = await _mediator.Send(new AddRecipeCommand(dto));
        if (result.Success) return Ok();
        
        return StatusCode(500, result.ErrorsList);
    } 

    [HttpPost("paged")]
    public async Task<ActionResult<PagedResult<RecipePreviewDto>>> GetRecipesPaged([FromBody] RecipesPreviewPageRequest dto)
    {
        var result = await _mediator.Send(new GetRecipesPagedQuery(dto));

        return Ok(result);
    }
    
    [HttpPost("tags")]
    public async Task<ActionResult> AddTag([FromBody] AddTagDto dto)
    {
        var result = await _mediator.Send(new AddTagCommand(dto));
        if (result.Success) return Ok();
        
        return StatusCode(500, result.ErrorsList);
    }
    
    [HttpGet("tags")]
    
    public async Task<ActionResult<TagListDto>> GetTags()
    {
        var result = await _mediator.Send(new GetTagsQuery());
        
        if (result.Success) return Ok(result.Data);
        
        return StatusCode(500, result.ErrorsList);
    }
    

    [HttpPost("ingredients")]
    public async Task<ActionResult> AddIngredient([FromBody] AddIngredientDto dto)
    {
        var result = await _mediator.Send(new AddIngredientCommand(dto));

        if (result.Success) return Ok();

        return StatusCode(500, result.ErrorsList);
    }
    
    [HttpGet("ingredients")]
    public async Task<ActionResult<IngredientListDto>> GetIngredients()
    {
        var result = await _mediator.Send(new GetIngredientsQuery());

        if (result.Success) return Ok(result.Data);

        return StatusCode(500, result.ErrorsList);
    }
}