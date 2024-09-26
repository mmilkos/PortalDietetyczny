using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.Application._Queries.Shop;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/shop")]
public class ShopController(IMediator mediator) : ControllerBase
{
    private IMediator _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<CartSummaryResponse>> GetSummary([FromBody] CartSummaryRequest dto)
    {
        var result = await _mediator.Send(new GetCartSummaryQuery(dto));

        if (result.Success)
        {
            return Ok(result.Data);
        }

        return StatusCode(500, result.ErrorsList);
    }
    
}