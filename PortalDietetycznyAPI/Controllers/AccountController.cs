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
public class AccountController(IMediator mediator) : ControllerBase
{
    private IMediator _mediator = mediator;


    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserRequestDto dto)
    {
        var result = await _mediator.Send(new LoginCommand(dto));

        if (result.Success)
        {
            Response.Cookies.Append("token", result.Data.Token, result.Data.CookieOptions);
            
            return Ok();
        }

        return Unauthorized();
    }
    
    [HttpPost("logout")]
    public ActionResult Logout()
    {
        var cookieOptions = new CookieOptions() 
        { 
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
        };
       Response.Cookies.Delete("token", cookieOptions);
       return Ok();
    }

    [Authorize]
    [HttpPost()]
    public ActionResult Authorize()
    {
        return Ok();
    }
}