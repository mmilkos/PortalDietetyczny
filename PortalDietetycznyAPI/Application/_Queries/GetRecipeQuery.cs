using MediatR;
using PagedList;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries;

public class GetRecipeQuery : IRequest<OperationResult<RecipeDetailsDto>>
{
    internal string Url;
    
    public GetRecipeQuery(string url)
    {
        Url = url;
    }
}

public class GetRecipeQueryHandler : IRequestHandler<GetRecipeQuery, OperationResult<RecipeDetailsDto>>
{
    private readonly IPDRepository _repository;
    
    public GetRecipeQueryHandler(IPDRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OperationResult<RecipeDetailsDto>> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<RecipeDetailsDto>()
        { };

        var uid = int.Parse(request.Url.Split('-').Last(), System.Globalization.NumberStyles.HexNumber);
        
        var recipe = await _repository.GetRecipe(uid);

        if (recipe == null || recipe.Url != request.Url)
        {
            operationResult.AddError(ErrorsRes.RecipeNotFound);
            return operationResult;
        }

        var ingredients = recipe.Ingredients.Select(ingredient => new RecipeIngredientDto()
            {
                Id = ingredient.Id,
                Name = ingredient.Ingredient.Name,
                Unit = ingredient.Unit,
                UnitValue = ingredient.UnitValue,
                HomeUnit = ingredient.HomeUnit,
                HomeUnitValue = ingredient.HomeUnitValue
            })
            .ToList();

        operationResult.Data = new RecipeDetailsDto()
        {
            Uid = recipe.Uid,
            RecipeTags = recipe.RecipeTags.Select(rt => rt.Tag.Name).ToList(),
            Name = recipe.Name,
            Nutrition = recipe.Nutrition,
            Ingredients = ingredients,
            Instruction = recipe.Instruction,
            PhotoUrl = recipe.Photo?.Url
        };

        return operationResult;
    }
}