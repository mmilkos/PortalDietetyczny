using System.Text;
using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Sharing;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands.Files;

public class UploadFileCommand : IRequest<OperationResult<StoredFile>>
{
    public AddFileDto Dto { get; private set; }
    
    public UploadFileCommand(AddFileDto dto)
    {
        Dto = dto;
    }
}

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, OperationResult<StoredFile>>
{
    private readonly IMediator _mediator;
    private readonly IKeyService _keyService;

    public UploadFileCommandHandler(IMediator mediator, IKeyService keyService)
    {
        _mediator = mediator;
        _keyService = keyService;
    }
    
    public async Task<OperationResult<StoredFile>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<StoredFile>();

        var token = await _keyService.GetDropBoxToken();
        
        var dbx = new DropboxClient(token.Access_token); 

        var filePath = $"/Dokumenty/{request.Dto.FileName}";
        
        var fileBytes = Convert.FromBase64String(request.Dto.FileBytes);

        var id = "";

        try
        { 
            id = await UploadToDropBoxAsync(fileBytes, filePath, dbx);
        }
        catch (AuthException  e)
        {
            operationResult.AddError(ErrorsRes.DropboxUploadError);
            return operationResult;
        }

        if (id.IsNullOrEmpty())
        {
            operationResult.AddError(ErrorsRes.DropboxUploadError);
            return operationResult;
        }

        var file = new StoredFile()
        {
            Name = request.Dto.FileName,
            DropboxId = id,
            FileType = request.Dto.FileType
        };

        operationResult.Data = file;

        return operationResult;

        /*using (dbx)
        {
            try
            {
                var response = await dbx.Files.DownloadAsync(id);
                var metadata = response.Response;

                using (var fileStream = File.Create(@"C:\Users\matmi\Desktop\TEST\" + metadata.Name + ".pdf"))
                {
                    var stream = await response.GetContentAsStreamAsync();
                    await stream.CopyToAsync(fileStream);
                }

                Console.WriteLine("Plik pobrany pomyślnie!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania pliku: {ex.Message}");
            }
        }*/
    }

    private async Task<string> UploadToDropBoxAsync(byte[] fileBytes, string filePath, DropboxClient dbc)
    {
        var id = "";
        
        using (var memStream = new MemoryStream(fileBytes))
        {
            var uploadArg = new UploadArg(filePath, WriteMode.Overwrite.Instance); 

            var result = await dbc.Files.UploadAsync(uploadArg, memStream);

            id = result.Id;
        }

        return id;
    }
}