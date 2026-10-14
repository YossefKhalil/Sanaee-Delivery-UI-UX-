using Core.Application.Common.Models;
using Core.Application.Features.OrderStatusHistories.Dtos;
using Core.Application.Features.OrderStatusHistories.Queries.GetAllHistoriesForAdmin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[contorller]")]
    public class OrderStatusHistoriesController : ControllerBase
    {
        private readonly ISender _mediator;

        public OrderStatusHistoriesController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("admin")]
        public async Task<ActionResult<PagedResult<GetHistoryDto>>> GetAllHistories([FromQuery] GetAllHistoriesForAdminQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
