using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/recipes")]
public class RecipesController : ControllerBase
{

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
}