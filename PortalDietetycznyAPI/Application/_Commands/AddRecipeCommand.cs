using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands;

public class AddRecipeCommand : IRequest<OperationResult<Unit>>
{
    public AddRecipeDto Dto { get; }
    
    public AddRecipeCommand(AddRecipeDto dto)
    {
        Dto = dto;
    }
}

public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand, OperationResult<Unit>>
{
    private IPDRepository _repository;
    private IMediator _mediator;

    public AddRecipeCommandHandler(IPDRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task<OperationResult<Unit>> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<Unit>();
        var dto = request.Dto;

        var photoResult = await GetPhoto(dto.File, cancellationToken);

        if (photoResult.Success == false)
        {
            result.AddErrorRange(photoResult.ErrorsList);
            return result;
        }

        var recipe = new Recipe
        {
            Name = dto.Name,
            Nutrition = dto.NutritionInfo,
            Instruction = dto.Instruction,
            PhotoId = photoResult.Data?.Id,
            Photo = photoResult.Data
        };

        await _repository.AddAsync(recipe);

        recipe.RecipeTags =  await GetRecipeTags(dto.TagsIds, recipe);
        
        recipe.Ingredients = await GetRecipeIngredients(dto.Ingredients, recipe);

        try
        {
            await _repository.UpdateAsync(recipe);
        }
        catch (Exception e)
        {
            result.AddError("Błąd przy update przepisu");
            return result;
        }
        
        return result;
    }
    
    private async Task<OperationResult<Photo>> GetPhoto(IFormFile? file, CancellationToken cancellationToken)
    {
        if (file != null) return await _mediator.Send(new AddPhotoCommand(file), cancellationToken);

        return new OperationResult<Photo>()
        {
            Data = null
        };
    }

    private async Task<List<RecipeTag>> GetRecipeTags(List<int> tagsIds, Recipe recipe)
    {
        List<RecipeTag> recipeTags = [];
        var tags = await _repository.GetAllEntitiesAsync<Tag>(tag => tagsIds.Contains(tag.Id));

        foreach (var tag in tags)
        {
            var recipeTag = new RecipeTag()
            {
                Recipe = recipe,
                RecipeId = recipe.Id,
                Tag = tag,
                TagId = tag.Id
            };
            
            recipeTags.Add(recipeTag);
        }

        return recipeTags;
    }

    private async Task<List<RecipeIngredient>> GetRecipeIngredients(List<RecipeIngredientDto> ingredientDtos, Recipe recipe)
    {
        var ingredientIds = ingredientDtos.Select(i => i.Id).ToList();

        var ingredients = await _repository.GetAllEntitiesAsync<Ingredient>(i => ingredientIds.Contains(i.Id));

        var recipeIngredients = new List<RecipeIngredient>();

        foreach (var ingredientDto in ingredientDtos)
        {
            var ingredient = ingredients.FirstOrDefault(i => i.Id == ingredientDto.Id);
            
            var recipeIngredient = new RecipeIngredient
            {
                RecipeId = recipe.Id,
                Ingredient = ingredient,
                IngredientId = ingredientDto.Id,
                Unit = ingredientDto.Unit,
                UnitValue = ingredientDto.UnitValue,
                HomeUnit = ingredientDto.HomeUnit,
                HomeUnitValue = ingredientDto.HomeUnitValue
            };

            recipeIngredients.Add(recipeIngredient);
        }

        return recipeIngredients;
    }
}