using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.DTOs;
using PortalDietetycznyAPI.Infrastructure.Repositories;
using PortalDietetycznyAPI.Tests.Common;

namespace PortalDietetycznyAPI.Tests._Commands;

public class AddTagCommandHandlerTests : BaseTests
{
    [Fact]
    public async Task ValidRequest_ShouldAddTag()
    {
        //Arrange
        var repository = new PdRepository(_dbContext);

        var dto = new AddTagDto()
        {
            Name = "Test"
        };
        
        var command = new AddTagCommand(dto);
        var handler = new AddTagCommandHandler(repository);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        var tagInDb = await _dbContext.Tags.FirstOrDefaultAsync();

        //Assert
        result.Should().BeOfType<OperationResult<Unit>>();
        result.Success.Should().BeTrue();
        tagInDb.Should().NotBeNull();
        tagInDb.Name.Should().Be(dto.Name);
    }
}