using System.Drawing;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Infrastructure.Repositories;
using PortalDietetycznyAPI.Tests.Common;


namespace PortalDietetycznyAPI.Tests._Commands;

/*public class AddPhotoCommandHandlerTests : BaseTests
{
    [Fact(Skip = "Skip because its uploading a picture")]
    public async Task AddPhotoCommandHandler_Should_Return_Success_Result()
    {
        // Arrange
        var repository = new PdRepository(_dbContext);
        
        using var bitmap = new Bitmap(100, 100);
        
        for (int x = 0; x < bitmap.Width; x++)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                bitmap.SetPixel(x, y, Color.Brown);
            }
        }
        
        byte[] imageBytes;
        using (var stream = new MemoryStream())
        {
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            imageBytes = stream.ToArray();
        }
        
        using var fileStream = new MemoryStream(imageBytes);
        
        IFormFile formFile = new FormFile(fileStream, 0, imageBytes.Length, "Image", "test.jpg")
        {
            Headers = new HeaderDictionary() { { "Content-Type", new StringValues("image/jpeg") } }
        };

        var options = _provider.GetRequiredService<IOptions<CloudinarySettings>>();
        
        var command = new AddRecipePhotoCommand(formFile);
        var handler = new UploadPhotoCommandHandler(options, repository);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        var photo = await _dbContext.Photos.FirstOrDefaultAsync();

        // Assert
        result.Success.Should().BeTrue();
        photo.Should().NotBe(null);
    }
}*/