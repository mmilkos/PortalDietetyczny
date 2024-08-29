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

    [HttpGet]
    public async Task<ActionResult<PagedResult<RecipePreviewDto>>> GetRecipesPaged()
    {
        var result = new PagedResult<RecipePreviewDto>()
        {
            PageNumber = 1,
            TotalCount = 10,
            Data = new List<RecipePreviewDto>()
            {
                new RecipePreviewDto()
                {
                    Id = 1,
                    Kcal = 100,
                    Name = "test 1",
                    Carb = 101,
                    Fat = 102,
                    Protein = 103
                },
                new RecipePreviewDto()
                {
                    Id = 2,
                    Kcal = 200,
                    Name = "test 2",
                    Carb = 201,
                    Fat = 202,
                    Protein = 203
                },
            }
        };

        return Ok(result);
    }

    public async Task<ActionResult> AddRecipe([FromBody] AddRecipeDto dto)
    {
        var result = await _mediator.Send(new AddRecipeCommand(dto));
        if (result.Success) return Ok();
        
        return StatusCode(500, result.ErrorsList);
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