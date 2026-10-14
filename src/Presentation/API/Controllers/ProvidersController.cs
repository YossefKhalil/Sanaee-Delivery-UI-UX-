using Core.Application.Features.Provideres.Queries.GetProviderByIdForAdmin;
using Core.Application.Common.Models;
using Core.Application.Features.ProviderCategories.Dtos;
using Core.Application.Features.Provideres.Commands.ChangeAvailability;
using Core.Application.Features.Provideres.Commands.CreateProvider;
using Core.Application.Features.Provideres.Dtos;
using Core.Application.Features.Provideres.Queries.GetAllProviders;
using Core.Application.Features.Provideres.Queries.GetProviderById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvidersController : ControllerBase
    {
        private readonly ISender _mediator;
        public ProvidersController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateProvider([FromBody] CreateProviderCommand command)
        {
            int result = await _mediator.Send(command);
            return CreatedAtAction
                (
                    nameof(GetProviderById),
                    new { id = result},
                    new {id  = result, message = "Provider created successfully."}
                );
        }


        [HttpPatch("{Id}")]
        public async Task<ActionResult> ChangeAvailability(int Id)
        {
            var command = new ChangeAvailabilityCommand(Id);
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ProviderDto>>> GetAllProviders([FromQuery] GetAllProvidersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<ProviderDto>> GetProviderById(int Id)
        {
            var query = new GetProviderByIdQuery(Id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("admin/{id}")]
        public async Task<ActionResult<GetProviderForAdminDto>> GetProviderByIdForAdmin(int id)
        {
            var query = new GetProviderByIdForAdminQurey(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
