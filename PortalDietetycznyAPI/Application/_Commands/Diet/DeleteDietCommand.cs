﻿using MediatR;
using PortalDietetycznyAPI.Application._Commands.Files;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;

namespace PortalDietetycznyAPI.Application._Commands;

public class DeleteDietCommand : IRequest<OperationResult<Unit>>
{
    public int DietId { get; private set; }
    
    public DeleteDietCommand(int dietId)
    {
        DietId = dietId;
    }
}

public class DeleteDietCommandHandler : IRequestHandler<DeleteDietCommand,OperationResult<Unit> >
{
    private readonly IPDRepository _repository;
    private readonly IMediator _mediator;
    
    public DeleteDietCommandHandler(IPDRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task<OperationResult<Unit>> Handle(DeleteDietCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();

        var photo = await _repository.FindEntityByConditionAsync<DietPhoto>(bp => bp.DietId == request.DietId);
        

        var photoResult = await _mediator.Send(new DeletePhotoCommand(photo.PublicId), cancellationToken);

        if (photoResult.Success == false)
        {
            operationResult.AddErrorRange(photoResult.ErrorsList);
            return operationResult;
        }
        
        var diet = await _repository.FindEntityByConditionAsync<Diet>(d => d.Id == request.DietId);
        
        var fileResult = await _mediator.Send(new DeleteFileCommand(diet.StoredFileId));
        
        if (fileResult.Success == false)
        {
            operationResult.AddErrorRange(fileResult.ErrorsList);
            return operationResult;
        }

        try
        {
            await _repository.DeleteAsync<Diet>(request.DietId);
            await _repository.DeleteAsync<DietPhoto>(photo.Id);
            await _repository.DeleteAsync<StoredFile>(diet.StoredFileId);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.DietDeleteError);
        }

        return operationResult;
    }
}