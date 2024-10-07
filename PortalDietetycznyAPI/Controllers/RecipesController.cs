using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Application._Queries;
using PortalDietetycznyAPI.Domain.Common;
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

    [HttpGet("{link}")]
    public async Task<ActionResult<RecipeDetailsDto>> GetRecipe([FromRoute] string link)
    {
       
        var result = await _mediator.Send(new GetRecipeQuery(link));
        if (result.Success) return Ok(result.Data);
        
        return StatusCode(500, result.ErrorsList);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> AddRecipe([FromBody] AddRecipeDto dto)
    {
        var result = await _mediator.Send(new AddRecipeCommand(dto));
        if (result.Success) return Ok();
        
        return StatusCode(500, result.ErrorsList);
    } 
    
    [HttpGet]
    public async Task<ActionResult<NamesListDto>> GetRecipes()
    {
        var result = await _mediator.Send(new GetRecipesQuery() );
        if (result.Success) return Ok(result.Data);
        
        return StatusCode(500, result.ErrorsList);
    } 
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRecipe(int id)
    {
        var result = await _mediator.Send(new DeleteRecipeCommand(id));
        if (result.Success) return Ok();

        return StatusCode(500, result.ErrorsList);
    }

    [HttpPost("paged")]
    public async Task<ActionResult<PagedResult<RecipePreviewDto>>> GetRecipesPaged([FromBody] RecipesPreviewPageRequest dto)
    {
        var result = await _mediator.Send(new GetRecipesPagedQuery(dto));

        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("tags")]
    public async Task<ActionResult> AddTag([FromBody] AddTagDto dto)
    {
        var result = await _mediator.Send(new AddTagCommand(dto));
        if (result.Success) return Ok();
        
        return StatusCode(500, result.ErrorsList);
    }
    
    [HttpGet("tags")]
    public async Task<ActionResult<NamesListDto>> GetTags()
    {
        var result = await _mediator.Send(new GetTagsQuery(TagContext.Recipe));
        
        if (result.Success) return Ok(result.Data);
        
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
    [HttpPost("ingredients")]
    public async Task<ActionResult> AddIngredient([FromBody] AddIngredientDto dto)
    {
        var result = await _mediator.Send(new AddIngredientCommand(dto));

        if (result.Success) return Ok();

        return StatusCode(500, result.ErrorsList);
    }
    
    [HttpGet("ingredients")]
    public async Task<ActionResult<NamesListDto>> GetIngredients()
    {
        var result = await _mediator.Send(new GetIngredientsQuery());

        if (result.Success) return Ok(result.Data);

        return StatusCode(500, result.ErrorsList);
    }
    
    [Authorize]
    [HttpDelete("ingredients/{id}")]
    public async Task<ActionResult> DeleteIngredient(int id)
    {
        var result = await _mediator.Send(new DeleteIngredientCommand(id));

        if (result.Success) return Ok();

        return StatusCode(500, result.ErrorsList);
    }
}