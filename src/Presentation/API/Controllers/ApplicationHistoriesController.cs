using Core.Application.Features.ApplicationHistories.Commands.CreateApplicationHistory;
using Core.Application.Features.ApplicationHistories.Dtos;
using Core.Application.Features.ApplicationHistories.Queries.GetProviderApplicationHistories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationHistoriesController : ControllerBase
    {
        private readonly ISender _mediator;

        public ApplicationHistoriesController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("{AdminId}")]
        public async Task<ActionResult> CreateApplicationHistory([FromBody] ApplicationHistoryDto dto, int AdminId)
        {
            var command = new CreateApplicationHistoryCommand(dto, AdminId);

            var ApplicationId = await _mediator.Send(command);
            return Created(string.Empty, new { HistroyId = ApplicationId, Message = "Application History Created Successfully." });
        }


        [HttpGet("{ProviderId}")]
        public async Task<ActionResult<GetApplicationHistoriesDto>> GetProviderApplicationHistories(int ProviderId)
        {
            var query = new GetProviderApplicationHistoriesQuery(ProviderId);

            var histories = await _mediator.Send(query);
            return Ok(histories);
        }
    }
}
