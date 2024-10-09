using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalDietetycznyAPI.Application._Commands.Shop;
using PortalDietetycznyAPI.Application._Queries.Shop;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Controllers;

[Controller]
[Route("api/shop")]
public class ShopController(IMediator mediator, IEmailService emailService, IKeyService keyService) : ControllerBase
{
    private IMediator _mediator = mediator;
    private IEmailService _emailService = emailService;
    private IKeyService _keyService = keyService;

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
        
        if (status == OrderStatus.COMPLETED) _mediator.Send(new ChangeOrderStatusCommand(requestDto.orderId, OrderStatus.COMPLETED));

        var portalSettings = _keyService.GetPortalSettings().Result;
        
        RecurringJob.AddOrUpdate(() => _emailService.SendEmailsJob(portalSettings), Cron.MinuteInterval(10));

        return Ok();
    }
    
    [HttpPost("order")]
    public async Task<ActionResult> StartOrder([FromBody] OrderDto dto)
    {
        if (ModelState.IsValid == false) return BadRequest();
        
        var startOrderResult = await _mediator.Send(new StartOrderCommand(dto));
        
        var order = startOrderResult.Data;

         if (startOrderResult.Success) await _mediator.Send(new SendPaymentRequestCommand(order));

        return Ok();
    }
    
    [Authorize]
    [HttpPost("orders/paged")]
    public async Task<ActionResult<PagedResult<OrderSummaryDto>>> GetOrdersPaged([FromBody] OrdersSummaryRequestDto dto)
    {
        var result = await _mediator.Send(new GetOrdersSummaryQuery(dto));

        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("order/invoice/{id}")]
    public async Task<ActionResult> GetOrdersInvoice([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetOrdersInvoiceQuery(id));

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