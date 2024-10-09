using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;
using Flurl.Http;
using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Enums;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Files;

public class GetFileQuery : IRequest<OperationResult<FileDto>>
{
    public int Id { get; private set; }
    
    public GetFileQuery(int id)
    {
        Id = id;
    }
}

public class GetFileQueryHandler : IRequestHandler<GetFileQuery, OperationResult<FileDto>>
{
    private readonly IPDRepository _repository;
    private readonly IMediator _mediator;
    
    
    public GetFileQueryHandler(IPDRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task<OperationResult<FileDto>> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<FileDto>();

        var file = await _repository.FindEntityByConditionAsync<StoredFile>(
            file => file.Id == request.Id && file.FileType == FileType.Downloadable);
        
        if (file == null)
        {
            operationResult.AddError("File not found");
            return operationResult;
        }

        var fileDto = await _mediator.Send(new DownloadFileQuery(file));

        operationResult.Data = fileDto.Data;
        return operationResult;
    }
}
