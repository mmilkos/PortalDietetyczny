using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/account")]
public class AccountController(IMediator mediator) : ControllerBase
{
    private IMediator _mediator = mediator;
    

}