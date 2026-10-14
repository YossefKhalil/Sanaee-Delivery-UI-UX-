using Core.Application.Common.Models;
using Core.Application.Features.Walletes.Commands.ActivateWallet;
using Core.Application.Features.Walletes.Commands.CreateWallet;
using Core.Application.Features.Walletes.Commands.DeActivateWallet;
using Core.Application.Features.Walletes.Commands.Deposit;
using Core.Application.Features.Walletes.Commands.Wthdrow;
using Core.Application.Features.Walletes.Dtos;
using Core.Application.Features.Walletes.Queries.GetAllWalletsForAdmin;
using Core.Application.Features.Walletes.Queries.GetWalletById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {

        private readonly ISender _mediator;
        public WalletsController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("{id:int}")]
        public async Task<ActionResult<int>> CreateWallt(int id)
        {
            var command = new CreateWalletCommand(id);
            var result = await _mediator.Send(command);

            return CreatedAtAction
                (
                    nameof(GetWalletById),
                    new {id  = result},
                    new { WalletId = result, message = "Wallet created successfully" }
                );
        }



        [HttpPut("deposit/{id}")]
        public async Task<ActionResult> Deposit([FromBody] WalleTransactiontDto dto, int id)
        {
            var command = new DepositCommand(dto, id);
            await _mediator.Send(command);

            return NoContent();
        }



        [HttpPut("withdrow/{id}")]
        public async Task<ActionResult> Withdrow([FromBody] WalleTransactiontDto dto, int id)
        {
            var command = new WithdrowCommand(dto, id);
            await _mediator.Send(command);

            return NoContent();
        }


        [HttpPatch("Activate/{id}")]
        public async Task<ActionResult> ActivateWallet([FromQuery]  int WalletId, int id)
        {
            string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            }

            string? userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

            var command = new ActivateWalletCommand(WalletId, id, ipAddress ?? "Unknown", userAgent ?? "Unknown");

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpPatch("DeActivate/{id}")]
        public async Task<ActionResult> DeActivateWallet([FromQuery] int WalletId, int id)
        {
            string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            }

            string? userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

            var command = new DeActivateWalletCommand(WalletId, id, ipAddress ?? "Unknown", userAgent ?? "Unknown");

            await _mediator.Send(command);
            return NoContent();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<decimal>> GetWalletById([FromQuery] int WalletId, int id)
        {
            var result = await _mediator.Send(new GetWalletByIdQuery(WalletId, id));
            return result;
        }



        [HttpGet("Admin")]
        public async Task<ActionResult<PagedResult<WalletDto>>> GetAllWallets([FromQuery] GetAllWalletsForAdminQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
