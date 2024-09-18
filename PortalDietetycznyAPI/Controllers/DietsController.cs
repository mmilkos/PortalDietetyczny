using System.Reflection.Metadata;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.Application._Commands;
using PortalDietetycznyAPI.Application._Commands.Files;
using PortalDietetycznyAPI.Application._Queries;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Enums;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/diets")]
public class DietsController : ControllerBase
{
    
    private IMediator _mediator;

    public DietsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult> AddDiet()
    {
        var dto = new AddFileDto();

        dto.FileType = FileType.Diet;
        
        var result = await _mediator.Send(new UploadFileCommand(dto));
        if (result.Success) return Ok();
        
        return StatusCode(500, result.ErrorsList);
    } 
    
    [HttpGet]
    public async Task<ActionResult<NamesListDto>> GetTags()
    {
        var result = await _mediator.Send(new GetTagsQuery(TagContext.Diet));
        
        if (result.Success) return Ok(result.Data);
        
        return StatusCode(500, result.ErrorsList);
    }
}

