using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.DTOs;
using PortalDietetycznyAPI.Infrastructure.Repositories;
using PortalDietetycznyAPI.Tests.Common;

namespace PortalDietetycznyAPI.Tests._Commands;

public class AddRecipeCommandHandlerTests : BaseTests
{
    [Fact]
    public async Task ValidRequest_ShouldAddRecipe()
    {
        //Arrange
        var repository = new PdRepository(_dbContext);

        var ingredient = new Ingredient()
        {
            Name = "Test ingredient"
        };

        var tag1 = new Tag()
        {
            Name = "Tag1"
        };

        var tag2 = new Tag()
        {
            Name = "Tag2"
        };

        await _dbContext.Tags.AddRangeAsync([tag1, tag2]);

        await _dbContext.Ingredients.AddAsync(ingredient);
        await _dbContext.SaveChangesAsync();

        var dto = new AddRecipeDto()
        {
            TagsIds = [tag1.Id, tag2.Id],
            Name = "Test name",
            Instruction = "Test instruction",
            Ingredients = [new RecipeIngredientDto()
            {
                Id = ingredient.Id,
                Unit = "kg",
                UnitValue = 1.5f,
                HomeUnit = "szklanka",
                HomeUnitValue = 2.5f
                
            }],
            NutritionInfo = new NutritionInfo()
            {
                Carb = 10,
                Fat = 20,
                Fiber = 30,
                Kcal = 40,
                Protein = 50
            },
            FileName = "test",
            FileBytes = ""
        };
        
        

        var response = new OperationResult<Photo>()
        {
            Data = new Photo()
            {
                PublicId = "test1",
                Url = "Test2"
            }
        };

        mediatorMock.Setup(x => x.Send(It.IsAny<UploadPhotoCommand>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var command = new AddRecipeCommand(dto);

        var handler = new AddRecipeCommandHandler(repository, mediatorMock.Object);
        
        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        var recipeInDb = await _dbContext.Recipe
            .Include(r => r.Ingredients)
            .Include(r => r.RecipeTags)
            .ThenInclude(t => t.Tag)
            .FirstOrDefaultAsync();

        result.ErrorsList.Should().BeEmpty();
        result.Success.Should().BeTrue();
        recipeInDb.Should().NotBeNull();
        recipeInDb.Name.Should().Be(dto.Name);
        recipeInDb.Name.Should().Be(dto.Name);
        recipeInDb.Nutrition.Should().Be(dto.NutritionInfo);
        recipeInDb.Ingredients.Count().Should().Be(dto.Ingredients.Count);
        recipeInDb.Ingredients[0].IngredientId.Should().Be(dto.Ingredients[0].Id);
        recipeInDb.Ingredients[0].Unit.Should().Be(dto.Ingredients[0].Unit);
        recipeInDb.Ingredients[0].UnitValue.Should().Be(dto.Ingredients[0].UnitValue);
        recipeInDb.Ingredients[0].HomeUnit.Should().Be(dto.Ingredients[0].HomeUnit);
        recipeInDb.Ingredients[0].HomeUnitValue.Should().Be(dto.Ingredients[0].HomeUnitValue);
        recipeInDb.Nutrition.Carb.Should().Be(dto.NutritionInfo.Carb);
        recipeInDb.Nutrition.Fat.Should().Be(dto.NutritionInfo.Fat);
        recipeInDb.Nutrition.Kcal.Should().Be(dto.NutritionInfo.Kcal);
        recipeInDb.Nutrition.Fiber.Should().Be(dto.NutritionInfo.Fiber);
        recipeInDb.Nutrition.Protein.Should().Be(dto.NutritionInfo.Protein);
        recipeInDb.RecipeTags.Count().Should().Be(dto.TagsIds.Count());
        recipeInDb.RecipeTags[0].Tag.Id.Should().Be(dto.TagsIds[0]);
        recipeInDb.RecipeTags[1].Tag.Id.Should().Be(dto.TagsIds[1]);
    }
}