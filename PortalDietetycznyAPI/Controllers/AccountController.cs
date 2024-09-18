using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.Application._Commands.Account;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/account")]
public class AccountController(IMediator mediator, IKeyService keyService) : ControllerBase
{
    private IMediator _mediator = mediator;
    private IKeyService _keyService = keyService;

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserRequestDto dto)
    {
        var result = await _mediator.Send(new LoginCommand(dto));

        if (result.Success)
        {
            Response.Cookies.Append("token", result.Data.Token, result.Data.CookieOptions);
            
            return Ok();
        }

        return StatusCode(500);
    }
    
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
       Response.Cookies.Delete("JwtToken");
       return Ok();
    }

    /*
    [Authorize]
    */
    [HttpPost()]
    public async Task<ActionResult> IsLoggedIn()
    {
        return Ok();
    }
}