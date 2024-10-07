using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.Application._Commands.Shop;
using PortalDietetycznyAPI.Application._Queries.Shop;
using PortalDietetycznyAPI.Domain.Resources;
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

        if (result.Success) return Ok(result.Data);
        

        return StatusCode(500, result.ErrorsList);
    }

    [Consumes("application/xml")]
    [Produces("application/xml")]
    [HttpPost("itn")]
    public ActionResult<AutopayItnResponseDto> InstantNotifications(AutopayItnRequestDto requestDto)
    {
        var status = requestDto.status;

        if (status == OrderStatus.STARTED) _mediator.Send(new ChangeOrderStatusCommand(requestDto.orderId, OrderStatus.STARTED));

        if (status == OrderStatus.WAITING) _mediator.Send(new ChangeOrderStatusCommand(requestDto.orderId, OrderStatus.WAITING));
        
        if (status == OrderStatus.COMPLETED) _mediator.Send(new ChangeOrderStatusCommand(requestDto.orderId, OrderStatus.COMPLETED));

        return Ok();
    }
    
    [HttpPost("order")]
    public async Task<ActionResult> StartOrder([FromBody] OrderDto dto)
    {
        var startOrderResult = await _mediator.Send(new StartOrderCommand(dto));
        
        var order = startOrderResult.Data;

        if (startOrderResult.Success) await _mediator.Send(new SendPaymentRequestCommand(order));

        return Ok();
    }
}