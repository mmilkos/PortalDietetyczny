using Dropbox.Api;
using Flurl.Http;
using MediatR;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Queries.Files;

public class DownloadFileQuery : IRequest<OperationResult<FileDto>>
{
    public StoredFile StoredFile { get; private set; }
    
    public DownloadFileQuery(StoredFile storedFile)
    {
        StoredFile = storedFile;
    }
}

public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, OperationResult<FileDto>>
{
    private readonly IKeyService _keyService;

    public DownloadFileQueryHandler(IKeyService keyService)
    {
        _keyService = keyService;
    }

    public async Task<OperationResult<FileDto>> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<FileDto>();
        var file = request.StoredFile;
        
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