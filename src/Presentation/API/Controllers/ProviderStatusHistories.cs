using Core.Application.Features.ProviderStatusHistories.Commands.CreateProviderStatusHistory;
using Core.Application.Features.ProviderStatusHistories.Queries.GetProviderStatusHistories;
using Core.Application.Features.ProviderStatusHistories.Dto;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderStatusHistories : ControllerBase
    {
        private readonly ISender _mediator;
        public ProviderStatusHistories(ISender mediator)
        {
            _mediator = mediator;
        }


        // Auditing the changes in Provider Stauts by Admin
        [HttpPost("{AdminId}")]
        public async Task<ActionResult<int>> CreateProviderStatusHistory(ProviderStatusHistoryDto dto, int AdminId)
        {
            var command = new CreateProviderStatusHistoryCommand(dto, AdminId);

            int historyId = await _mediator.Send(command);
            return Created(string.Empty, new { Id = historyId, Message = "Provider Status History created successfully." });
        }


        // Get all Status Histories for a Provider to show it to Admin
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetProviderStatusHistoriesDto>>> GetProviderStatusHistories(int ProviderId)
        {
            var query = new GetProviderStatusHistoriesQuery(ProviderId);

            var histories = await _mediator.Send(query);
            return Ok(histories);
        }
    }
}
