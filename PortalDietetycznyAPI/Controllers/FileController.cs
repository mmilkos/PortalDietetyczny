using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.Application._Commands.Files;
using PortalDietetycznyAPI.Application._Queries.Files;
using PortalDietetycznyAPI.Domain.Enums;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/file")]
public class FileController : ControllerBase
{
    private IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /*[Authorize]*/
    [HttpPost]
    public async Task<ActionResult> AddFile([FromBody] AddFileDto dto)
    {
        dto.FileType = FileType.Downloadable;
        
        var result = await _mediator.Send(new AddFileCommand(dto));
        if (result.Success) return Ok();
        
        return StatusCode(500, result.ErrorsList);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> AddFile([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteFileCommand(id));
        if (result.Success) return Ok();
        
        return StatusCode(500, result.ErrorsList);
    }

    [HttpGet("downloadableFiles")]
    public async Task<ActionResult<NamesListDto>> GetAllFiles()
    {
        var result = await _mediator.Send(new GetAllFilesQuery(FileType.Downloadable));
        
        if (result.Success) return Ok(result.Data);

        return StatusCode(500, result.ErrorsList);
    }
    
    [HttpPost("{id}")]
    public async Task<ActionResult> GetFile([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetFileQuery(id));

        if (result.Success)
        { 
            return new FileStreamResult(result.Data.Stream, result.Data.MimeType)
            {
                FileDownloadName = result.Data.FileName
            };
        }
        
        return StatusCode(500, result.ErrorsList);
    }
}