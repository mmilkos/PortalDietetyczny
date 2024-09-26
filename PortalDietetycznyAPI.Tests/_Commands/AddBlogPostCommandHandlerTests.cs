using System.Text;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.DTOs;
using PortalDietetycznyAPI.Infrastructure.Repositories;
using PortalDietetycznyAPI.Tests.Common;

namespace PortalDietetycznyAPI.Tests._Commands;

public class AddBlogPostCommandHandlerTests : BaseTests
{
    [Fact]
    public async Task ValidRequest_ShouldAddBlogPost()
    {
        var repository = new PdRepository(_dbContext);

        var dto = new AddBlogPostDto()
        {
            Title = "testTitle",
            Content = "TestContent",
            FileBytes = Convert.ToBase64String("test"u8.ToArray()),
            FileName = "testFileName"
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

        var command = new AddBlogPostCommand(dto);

        var handler = new AddBlogPostCommandHandler(mediatorMock.Object, repository);

        var result = await handler.Handle(command, CancellationToken.None);

        var blogPostFromDb = await _dbContext.BlogPosts.FirstOrDefaultAsync();

        result.ErrorsList.Should().BeEmpty();
        result.Success.Should().BeTrue();
        blogPostFromDb.Should().NotBeNull();
        blogPostFromDb.Title.Should().Be(dto.Title);
        blogPostFromDb.Content.Should().Be(dto.Content);
        blogPostFromDb.Url.Should().NotBeNullOrEmpty();
    }
}