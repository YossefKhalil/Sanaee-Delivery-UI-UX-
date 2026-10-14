using Core.Application.Common.Models;
using Core.Application.Features.WalletTransactiones.Commands.CreateWalletTransaction;
using Core.Application.Features.WalletTransactiones.Dtos;
using Core.Application.Features.WalletTransactiones.Queries.GetAllTransactionsByWalletId;
using Core.Application.Features.WalletTransactiones.Queries.GetAllWalletTransactionsByAdmin;
using Core.Application.Features.WalletTransactiones.Queries.GetTransactionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletTransactionController : ControllerBase
    {
        private readonly ISender _mediator;

        public WalletTransactionController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateWalletTransaction([FromBody] CreateWalletTransactionCommand command)
        {
            var id = await _mediator.Send(command);
            return Created(string.Empty, new { Id = id, Message = "Transaction Created Successfully." });
        }


        [HttpGet("admin")]
        public async Task<ActionResult<PagedResult<GetWalletTransactionDto>>> GetAllWalletTransactionsByAdmin([FromQuery] GetAllWalletTransactionsByAdminQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetWalletTransactionDto>>> GetAllTransactionsByWalletId([FromBody] GetAllTransactionsByWalletIdQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetWalletTransactionDto>> GetTransactionById(int id)
        {
            var command = new GetTransactionByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
  