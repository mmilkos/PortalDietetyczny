using System.Globalization;
using System.Text;
using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
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

public class AddRecipeCommandHandler : IdentifierGenerator, IRequestHandler<AddRecipeCommand, OperationResult<Unit>>
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

        var fileBytes = Convert.FromBase64String(dto.FileBytes);

        var photoResult = await GetPhoto(fileBytes, dto.FileName , cancellationToken);

        if (photoResult.Success == false)
        {
            result.AddErrorRange(photoResult.ErrorsList);
            return result;
        }

        var photo = new RecipePhoto()
        {
            PublicId = photoResult.Data.PublicId,
            Url = photoResult.Data.Url
        };

        var uid = GenerateUid();

        var url = GenerateUrl(uid, dto.Name);

        var recipe = new Recipe
        {
            Uid = uid,
            Name = dto.Name,
            Nutrition = dto.NutritionInfo,
            Instruction = dto.Instruction,
            PhotoId = photo.Id,
            Photo = photo,
            Url = url
        };

        try
        {
            await _repository.AddAsync(recipe);
        }
        catch (Exception e)
        {
            result.AddError(ErrorsRes.RecipeSavingError);
            return result;
        }

        recipe.RecipeTags =  await GetRecipeTags(dto.TagsIds, recipe);
        
        recipe.Ingredients = await GetRecipeIngredients(dto.Ingredients, recipe);

        try
        {
            await _repository.UpdateAsync(recipe);
        }
        catch (Exception e)
        {
            result.AddError(ErrorsRes.RecipeUpdateError);
            return result;
        }
        
        return result;
    }
    
    private async Task<OperationResult<Photo>> GetPhoto(byte[] fileBytes, string fileName, CancellationToken cancellationToken)
    { 
        return await _mediator.Send(new UploadPhotoCommand(fileBytes, fileName, FoldersNamesRes.Recipes_photos), cancellationToken);
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
                Unit = ingredientDto.Unit.ToLower(),
                UnitValue = ingredientDto.UnitValue,
                HomeUnit = ingredientDto.HomeUnit.ToLower(),
                HomeUnitValue = ingredientDto.HomeUnitValue
            };

            recipeIngredients.Add(recipeIngredient);
        }

        return recipeIngredients;
    }
}