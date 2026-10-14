using Core.Application.Features.Addresses.Queries.GetAddressById;
using Core.Application.Features.Addresses.Dtos;
using Core.Application.Features.Addresses.Commands.CreateAddress;
using Core.Application.Features.Addresses.Commands.DeleteAddress;
using Core.Application.Features.Addresses.Commands.SetDefaultAddress;
using Core.Application.Features.Addresses.Commands.UpdateAddress;
using Core.Application.Features.Addresses.Queries.GetDefaultAddress;
using Core.Application.Features.Addresses.Queries.GetUserAddresses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly ISender _mediator;

        public AddressesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAddress([FromBody] CreateAddressCommand command)
        {
            var AddressId = await _mediator.Send(command);
            return CreatedAtAction
                (
                    nameof(GetAddressById),
                    new {id = AddressId},
                    new {id = AddressId, message = "Address created successfully"}
                );
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateAddress(int id, [FromBody] CreateUadateAddressDto dto)
        {
            var command = new UpdateAddressCommand(id, dto);

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAddress(int id)
        {
            await _mediator.Send(new DeleteAddressCommand(id));
            return NoContent();
        }


        [HttpPatch]
        [Route("{id}/set-default")]
        public async Task<ActionResult> SetDefaultAddress(int id, [FromQuery] int userId)
        {
            await _mediator.Send(new SetDefaultAddressCommand(id, userId));
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<AddressDto>>> GetUserAddresses(int id)
        {
            var result = await _mediator.Send(new GetUserAddressesQuery(id));
            return Ok(result);
        }


        [HttpGet("default")]
        public async Task<ActionResult<AddressDto>> GetDefaultAddress([FromQuery] int userId)
        {
            var result = await _mediator.Send(new GetDefaultAddressQuery(userId));
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<AddressDto>> GetAddressById([FromQuery]int Id)
        {
            var result = await _mediator.Send(new GetAddressByIdQuery(Id));
            return Ok(result);
        }
    }
}
