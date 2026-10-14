using Application.Features.CancelatoinReasones.Commands.CreateCancelationReason;
using Application.Features.CancelatoinReasones.Dto;
using Core.Application.Features.CancelatoinReasones.Commands.ActivateCancelationReason;
using Core.Application.Features.CancelatoinReasones.Commands.DeactivateCacelationReason;
using Core.Application.Features.CancelatoinReasones.Commands.DeleteCancelationReason;
using Core.Application.Features.CancelatoinReasones.Commands.UpdateCancelationReason;
using Core.Application.Features.CancelatoinReasones.Dto;
using Core.Application.Features.CancelatoinReasones.Queries.GetAdminCancelationReasons;
using Core.Application.Features.CancelatoinReasones.Queries.GetAllCancelationReasonByAdmin;
using Core.Application.Features.CancelatoinReasones.Queries.GetCancelationReasonById;
using Core.Application.Features.CancelatoinReasones.Queries.GetCustomerCancelationReasons;
using Core.Application.Features.CancelatoinReasones.Queries.GetProviderCancelationReasons;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CancelationReasonController : ControllerBase
    {
        private readonly ISender _mediator;

        public CancelationReasonController(ISender mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("{ReasonId}")]
        public async Task<ActionResult<GetCancelationReasonByIdDto>> GetCancelationReasonById(int ReasonId)
        {
            var query = new GetCancelationReasonByIdQuery(ReasonId);

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CancelationReason>>> GetAllCancelationReasonByAdmin()
        {
            var query = new GetAllCancelationReasonByAdminQuery();

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpGet("customer-reasons")]
        public async Task<ActionResult<IReadOnlyList<GetUserCancelationReasonDto>>> GetCustomerCancelationReasons()
        {
            var query = new GetCustomerCancelationReasonsQuery();

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpGet("provider-reasons")]
        public async Task<ActionResult<IReadOnlyList<GetUserCancelationReasonDto>>> GetProviderCancelationReasons()
        {
            var query = new GetProviderCancelationReasonQuery();

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpGet("Admin-reasons")]
        public async Task<ActionResult<IReadOnlyList<GetUserCancelationReasonDto>>> GetAdminCancelationReasons()
        {
            var query = new GetAdminCancelationReasonsQuery();

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpPost]
        public async Task<ActionResult<int>> CreateCancelationReason([FromBody] CreateCancelationReasonCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCancelationReasonByIdQuery), new {id = result}, new {id = result, message = "Cancelation Reason Created Successfully."});
        }




        [HttpPut]
        public async Task<ActionResult> UpdateCancelationReason([FromBody] UpdateCancelationReasonCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }



        [HttpDelete]
        public async Task<ActionResult> DeleteCancelationReason([FromBody] DeleteCancelationReasonCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("Activate/{ReasonId}")]
        public async Task<ActionResult> ActivateCancelationReason(int ReasonId)
        {
            var command = new ActivateCancelationReasonCommand(ReasonId);

            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("Deactivate/{ReasonId}")]
        public async Task<ActionResult> DeactivateCancelationReason(int ReasonId)
        {
            var command = new DeactivateCancelationReasonCommand(ReasonId);

            await _mediator.Send(command);
            return NoContent();
        }



    }
}
