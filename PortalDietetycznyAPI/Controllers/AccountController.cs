using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/account")]
public class AccountController(IMediator mediator, IKeyService keyService) : ControllerBase
{
    private IMediator _mediator = mediator;
    private IKeyService _keyService = keyService;

    [HttpPost("startApp")]
    public async Task<ActionResult> StartApp()
    {
        _keyService.GetVaultTokenJob();
        RecurringJob.AddOrUpdate(()=> _keyService.GetVaultTokenJob(), Cron.Hourly);
        
        await _keyService.GetCloudinarySettingsAsync();
        
        return Ok();
    }
}