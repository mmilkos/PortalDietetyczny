using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Options;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;

namespace PortalDietetycznyAPI.Application._Commands;

public class AddPhotoCommand : IRequest<OperationResult<Photo>>
{
    public byte[] FileBytes { get; private set; }
    public string FileName { get; private set; }
    
    public AddPhotoCommand(byte[] fileBytes, string fileName )
    {
        FileBytes = fileBytes;
        FileName = fileName;
    }
}

public class AddPhotoCommandHandler : IRequestHandler<AddPhotoCommand, OperationResult<Photo>>
{
    private readonly Cloudinary _cloudinary;
    private readonly IPDRepository _repository;

    public AddPhotoCommandHandler(IOptions<CloudinarySettings> config, IPDRepository repository)
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
    public async Task<OperationResult<Photo>> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Photo>() { };
        
        var file = GeneratePhoto(request.FileBytes, request.FileName);

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
            Folder = "Recipes_photos"
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

        var photo = new Photo()
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url.ToString()
        };

        try
        {
            await _repository.AddAsync(photo);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.PhotoSavingError);
            return operationResult;
        }
        
        operationResult.Data = photo;
        
        return operationResult;
    }
    
    private FormFile GeneratePhoto(byte[] fileBytes, string fileName)
    {
        var stream = new MemoryStream(fileBytes);
        var formFile = new FormFile(
            baseStream: stream, 
            baseStreamOffset: 0, 
            length: fileBytes.Length,
            name: "",
            fileName: fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };
        
        return formFile;
    }
}

