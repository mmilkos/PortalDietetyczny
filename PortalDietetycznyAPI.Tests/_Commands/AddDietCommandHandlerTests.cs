using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Application._Commands.Files;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Enums;
using PortalDietetycznyAPI.DTOs;
using PortalDietetycznyAPI.Infrastructure.Repositories;
using PortalDietetycznyAPI.Tests.Common;

namespace PortalDietetycznyAPI.Tests._Commands;

public class AddDietCommandHandlerTests : BaseTests
{
    [Fact]
    public async Task ValidRequest_ShouldAddDiet()
    {
        //Arrange
        var repository = new PdRepository(_dbContext);

        var tag = new Tag()
        {
            Name = "TestTag",
            Context = TagContext.Diet,
        };

        await _dbContext.Tags.AddAsync(tag);
        await _dbContext.SaveChangesAsync();


        var list = new List<int>() { tag.Id };

        var dto = new AddDietDto()
        {
            Name = "testName",
            TagsIds = list,
            Kcal = 100,
            FileBytes = "",
            FileName = "testFileName",
            PhotoFileBytes = "",
            PhotoFileName = "testPhotoFileName"
        };
        
        var photoResponse = new OperationResult<Photo>()
        {
            Data = new Photo()
            {
                PublicId = "test1",
                Url = "Test2"
            }
        };
        
        var fileResponse = new OperationResult<StoredFile>()
        {
            Data = new StoredFile()
            {
                Name = "DietPdf",
                DropboxId = "dropBoxID",
                FileType = FileType.Diet,
            }
        };
        
        var mock = new Mock<IMediator>();

        mock.Setup(x => x.Send(It.IsAny<UploadPhotoCommand>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(photoResponse);
        
        mock.Setup(x => x.Send(It.IsAny<UploadFileCommand>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(fileResponse);
        
        

        var command = new AddDietCommand(dto);
        var handler = new AddDietCommandHandler(repository, mock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        var dietInDb = await _dbContext.Diets
            .Include(d => d.Photo)
            .Include(d => d.DietTags)
            .Include(diet => diet.File)
            .FirstOrDefaultAsync();
        
        result.ErrorsList.Should().BeEmpty();
        result.Success.Should().BeTrue();
        dietInDb.Should().NotBeNull();
        dietInDb.Name.Should().Be(dto.Name);
        dietInDb.Kcal.Should().Be(dto.Kcal);
        dietInDb.Photo.PublicId.Should().Be(photoResponse.Data.PublicId);
        dietInDb.Photo.Url.Should().Be(photoResponse.Data.Url);
        dietInDb.DietTags.FirstOrDefault().DietId.Should().Be(dietInDb.Id);
        dietInDb.File.Should().Be(fileResponse.Data);
        dietInDb.StoredFileId.Should().Be(fileResponse.Data.Id);
    }
    
}