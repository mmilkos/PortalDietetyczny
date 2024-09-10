using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Options;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;

namespace PortalDietetycznyAPI.Application._Commands;

public class AddBlogPhotoCommand : IRequest<OperationResult<BlogPhoto>>
{
    public byte[] FileBytes { get; private set; }
    public string FileName { get; private set; }
    
    public AddBlogPhotoCommand(byte[] fileBytes, string fileName)
    {
        FileBytes = fileBytes;
        FileName = fileName;
    }
}

public class AddBlogPhotoCommandHandler : PhotoGenerator, IRequestHandler<AddBlogPhotoCommand, OperationResult<BlogPhoto>>
{
    private readonly Cloudinary _cloudinary;
    private readonly IPDRepository _repository;

    public AddBlogPhotoCommandHandler(IOptions<CloudinarySettings> config, IPDRepository repository)
    {
        _repository = repository;
        
        var account = new Account()
        {
            Cloud = config.Value.CloudName,
            ApiKey = config.Value.ApiKey,
            ApiSecret = config.Value.ApiSecret
        };
        
        _cloudinary = new Cloudinary(account);
    }
    
    
    public async Task<OperationResult<BlogPhoto>> Handle(AddBlogPhotoCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<BlogPhoto>() { };
        
        var file = GeneratePhoto(request.FileBytes, request.FileName);

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
            Folder = FoldersNamesRes.Blog_photos
        };

        ImageUploadResult uploadResult;

        try
        { 
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.CloudinaryError);
            return operationResult;
        }
        
        var blogPhoto = new BlogPhoto
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url.ToString()
        };

        try
        {
            await _repository.AddAsync(blogPhoto);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.PhotoSavingError);
            return operationResult;
        }

        operationResult.Data = blogPhoto;
        
        return operationResult;
    }
}