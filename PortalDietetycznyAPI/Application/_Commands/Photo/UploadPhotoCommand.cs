using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Options;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;

namespace PortalDietetycznyAPI.Application._Commands;

public class UploadPhotoCommand : IRequest<OperationResult<Photo>>
{
    public byte[] FileBytes { get; private set; }
    public string FileName { get; private set; }
    
    public string FolderName { get; private set; }
    
    public UploadPhotoCommand(byte[] fileBytes, string fileName, string folderName)
    {
        FileBytes = fileBytes;
        FileName = fileName;
        FolderName = folderName;
    }
}

public class UploadPhotoCommandHandler : PhotoGenerator, IRequestHandler<UploadPhotoCommand, OperationResult<Photo>>
{
    private readonly IKeyService _keyService;
    public UploadPhotoCommandHandler(IKeyService keyService)
    {
        _keyService = keyService;
    }
    
    public async Task<OperationResult<Photo>> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
    {
        var settings = await _keyService.GetCloudinarySettingsAsync();
        
        var account = new CloudinaryDotNet.Account()
        {
            Cloud = settings.CloudName,
            ApiKey = settings.ApiKey,
            ApiSecret = settings.ApiSecret
        };
        
        var cloudinary = new Cloudinary(account);
        
        
        var operationResult = new OperationResult<Photo>() { };
        
        var file = GeneratePhoto(request.FileBytes, request.FileName);

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
            Folder = request.FolderName
        };

        ImageUploadResult uploadResult;

        try
        { 
            uploadResult = await cloudinary.UploadAsync(uploadParams);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.CloudinaryError);
            return operationResult;
        }
        
        var photo = new Photo()
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url.ToString()
        };

        operationResult.Data = photo;
        
        return operationResult;
    }
}