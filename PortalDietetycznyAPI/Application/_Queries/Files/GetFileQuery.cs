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
    private readonly IKeyService _keyService;
    
    public GetFileQueryHandler(IPDRepository repository, IMediator mediator, IKeyService keyService)
    {
        _repository = repository;
        _mediator = mediator;
        _keyService = keyService;
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
        
        var token = await _keyService.GetDropBoxToken();
        
        var dbx = new DropboxClient(token.Access_token);

        var fileDto = new FileDto();
        
        using (dbx)
        {
            try
            {
                var response = await "https://content.dropboxapi.com/2/files/download"
                    .WithHeader("Authorization", $"Bearer {token.Access_token}")
                    .WithHeader("Dropbox-API-Arg", $"{{\"path\":\"{file.DropboxId}\"}}")
                    .PostAsync(cancellationToken: cancellationToken)
                    .ReceiveBytes();

                    var memoryStream = new MemoryStream(response);
                    
                    memoryStream.Position = 0;
                    fileDto.FileName = file.Name + ".pdf";
                    fileDto.Stream = memoryStream;
                    fileDto.MimeType = "application/pdf";
            }
            catch (Exception ex)
            {
                operationResult.AddError("Wystąpił błąd podczas pobierania pliku");
                return operationResult;
            }
        }

        operationResult.Data = fileDto;
        return operationResult;
    }
}
