using FluentAssertions;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.DTOs;
using PortalDietetycznyAPI.Infrastructure.Repositories;
using PortalDietetycznyAPI.Tests.Common;
using PortalDietetycznyAPI.Domain.Resources;

namespace PortalDietetycznyAPI.Tests._Commands;

public class AddIngredientCommandHandlerTests : BaseTests
{
    [Fact]
    public async Task ValidRequest_ShouldAddIngredient()
    {
        //Arrange
        var repository = new PdRepository(_dbContext);

        var dto = new AddIngredientDto()
        {
            Name = "Test123"
        };

        var command = new AddIngredientCommand(dto: dto);

        var handler = new AddIngredientCommandHandler(repository: repository);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        var ingredientInDb = _dbContext.Ingredients.FirstOrDefault();
        
        //Assert
        result.Success.Should().BeTrue();
        ingredientInDb.Should().NotBeNull();
        ingredientInDb.Name.Should().Be(dto.Name);
    }
    
    [Fact]
    public async Task IngredientAlreadyInDb_ShouldReturnError()
    {
        //Arrange
        var repository = new PdRepository(_dbContext);

        var ingredientInDb = await IngredientObjectMother.CreateAsync(_dbContext, name: "Test");

        var dto = new AddIngredientDto()
        {
            Name = "test"
        };

        var command = new AddIngredientCommand(dto: dto);

        var handler = new AddIngredientCommandHandler(repository: repository);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        var count = _dbContext.Ingredients.Count();
        
        
        //Assert
        result.ErrorsList.Should().Contain(ErrorsRes.IngredientAlreadyInDb);
        result.Success.Should().BeFalse();
        count.Should().Be(1);
    }
}