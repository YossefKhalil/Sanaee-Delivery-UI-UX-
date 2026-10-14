using Core.Application.Features.Users.Commands.DeleteUser;
using Core.Application.Features.Users.Commands.RestoreUser;
using Core.Application.Features.Users.Commands.UpdateUser;
using Core.Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Core.Application.Features.Users.Commands.DeActivateUser;
using Core.Application.Features.Users.Commands.ActivateUser;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _mediator;

        public UsersController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var command = new UpdateUserCommand(id, dto);

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            }

            string? userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

            var command = new DeleteUserCommand(id, ipAddress ?? "Unknown", userAgent ?? "Unknown");

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> RestoreUser([FromBody] CheckAcountDto dto, int id)
        {
            string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            }

            string? userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

            var command = new RestoreUserCommand(id, dto, ipAddress ?? "Unknown", userAgent ?? "Unknown");

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpPatch("activate")]
        public async Task<ActionResult> ActivateUser([FromQuery] int UserId)
        {
            var command = new ActivateUserCommand(UserId);

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpPatch("deactivate")]
        public async Task<ActionResult> DeActivateUser([FromQuery] int UserId)
        {
            var command = new DeActivateUserCommand(UserId);

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
