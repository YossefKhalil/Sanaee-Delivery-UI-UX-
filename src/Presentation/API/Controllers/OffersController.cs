using Core.Application.Features.Offeres.Commands.AcceptOffer;
using Core.Application.Features.Offeres.Commands.CancelOffer;
using Core.Application.Features.Offeres.Commands.CreateOffer;
using Core.Application.Features.Offeres.Commands.RejectOffer;
using Core.Application.Features.Offeres.Dtos;
using Core.Application.Features.Offeres.Queries.GetAcceptedOffer;
using Core.Application.Features.Offeres.Queries.GetAllOffersForOrder;
using Core.Application.Features.Offeres.Queries.GetOfferById;
using Core.Application.Features.Orderes.Queries.GetOrderById;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OffersController : ControllerBase
    {
        private readonly ISender _mediator;

        public OffersController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("accepted")]
        public async Task<ActionResult<GetOfferDto>> GetAcceptedOffer([FromQuery] int OrderId)
        {
            var query = new GetAcceptedOfferQuery(OrderId);

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("{OfferId}")]
        public async Task<ActionResult<GetOfferDto>> GetOfferById(int OfferId)
        {
            var query = new GetOfferByIdQuery(OfferId);

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetAllOffersForOrderDto>>> GetAllOffersForOrder([FromQuery] int OrderId)
        {
            var query = new GetAllOffersForOrderQuery(OrderId);

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<GetAllOffersForOrderDto>>> CreateOffer([FromBody] CreateOfferCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOfferById), new {id = result}, new {id = result, message = "Offer Created Successfully"});
        }



        [HttpPatch("reject/{OfferId}")]
        public async Task<ActionResult<IReadOnlyList<GetAllOffersForOrderDto>>> RejectOffer(int OfferId)
        {
            var command = new RejectOfferCommand(OfferId);

            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("cancel/{OfferId}")]
        public async Task<ActionResult<IReadOnlyList<GetAllOffersForOrderDto>>> CancelOffer(int OfferId)
        {
            var command = new CancelOfferCommand(OfferId);

            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("accept/{OfferId}")]
        public async Task<ActionResult<IReadOnlyList<GetAllOffersForOrderDto>>> AcceptOffer(int OfferId)
        {
            var command = new AcceptOfferCommand(OfferId);

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
