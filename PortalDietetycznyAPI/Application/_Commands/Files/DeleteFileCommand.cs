using Dropbox.Api;
using Dropbox.Api.Files;
using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;

namespace PortalDietetycznyAPI.Application._Commands.Files;

public class DeleteFileCommand : IRequest<OperationResult<Unit>>
{
    public int Id { get; private set; }
    public DeleteFileCommand(int id)
    {
        Id = id;
    }
}

public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, OperationResult<Unit>>
{
    private readonly IKeyService _keyService;
    private readonly IPDRepository _repository;
    
    public DeleteFileCommandHandler(IKeyService keyService, IPDRepository repository)
    {
        _keyService = keyService;
        _repository = repository;
    }
    
    public async Task<OperationResult<Unit>> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<Unit>();
        
        var token = await _keyService.GetDropBoxToken();
        
        var dbx = new DropboxClient(token.Access_token);

        var fileInDb = await _repository.FindEntityByConditionAsync<StoredFile>(file => file.Id == request.Id);

        if (fileInDb == null)
        {
            operationResult.AddError(ErrorsRes.FileNotFound);
            return operationResult;
        }
        
        try
        { 
            await _repository.DeleteAsync<StoredFile>(fileInDb.Id);
        }
        catch (Exception e)
        {
            operationResult.AddError(ErrorsRes.FileDeletingError);
            return operationResult;
        }
        
        return operationResult;
    }
}