using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Options;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands;

public class DeletePhotoCommand : IRequest<OperationResult<DeletionResult>>
{
    public string PublicID { get; private set; }
    
    public DeletePhotoCommand(string publicID)
    {
        PublicID = publicID;
    }
}

public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, OperationResult<DeletionResult>>
{
    private readonly Cloudinary _cloudinary;
    
    public DeletePhotoCommandHandler(IOptions<CloudinarySettings> config)
    {
        var account = new Account()
        {
            Cloud = config.Value.CloudName,
            ApiKey = config.Value.ApiKey,
            ApiSecret = config.Value.ApiSecret
        };
        
        _cloudinary = new Cloudinary(account);
        
    }
    
    public async Task<OperationResult<DeletionResult>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<DeletionResult>();
        
        var deleteParams = new DeletionParams(request.PublicID);

        DeletionResult deletionResult;

        try
        {
            deletionResult = await _cloudinary.DestroyAsync(deleteParams);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.CloudinaryError);
            return operationResult;
        }

        operationResult.Data = deletionResult;

        return operationResult;
    }
}