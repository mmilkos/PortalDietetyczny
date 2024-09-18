using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Options;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Interfaces;
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
    private readonly IKeyService _keyService;
    
    public DeletePhotoCommandHandler(IKeyService keyService)
    {
        _keyService = keyService;
    }
    
    public async Task<OperationResult<DeletionResult>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<DeletionResult>();
        
        var settings = await _keyService.GetCloudinarySettingsAsync();
        
        var account = new CloudinaryDotNet.Account()
        {
            Cloud = settings.CloudName,
            ApiKey = settings.ApiKey,
            ApiSecret = settings.ApiSecret
        };
        
        var cloudinary = new Cloudinary(account);
        
        
        var deleteParams = new DeletionParams(request.PublicID);

        DeletionResult deletionResult;

        try
        {
            deletionResult = await cloudinary.DestroyAsync(deleteParams);
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