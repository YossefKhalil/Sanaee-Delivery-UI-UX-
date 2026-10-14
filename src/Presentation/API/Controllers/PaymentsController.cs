using Core.Application.Features.Pymentes.Commands.CreatePayment;
using Core.Application.Features.Pymentes.Commands.MarkPaymentAsFailed;
using Core.Application.Features.Pymentes.Commands.MarkPaymentAsSuccess;
using Core.Application.Features.Pymentes.Commands.RefundPayment;
using Core.Application.Features.Pymentes.Queries.GetAllPaymentsByAdmin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ISender _mediator;

        public PaymentsController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            int id = await _mediator.Send(command);
            return Created(string.Empty, new { Id = id, Message = "Payment Created Successfully!" });
        }


        [HttpPatch("success")]
        public async Task<ActionResult> MarkPaymentAsSuccess([FromBody] MarkPaymentAsSuccessCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpPatch("failed")]
        public async Task<ActionResult> MarkPaymentAsFailed([FromBody] MarkPaymentAsFailedCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpPatch("refund")]
        public async Task<ActionResult> RefundPayment([FromBody] RefundPaymentCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("admin")]
        public async Task<ActionResult> GetAllPaymentsByAdmin([FromQuery] GetAllPaymentsByAdminQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
