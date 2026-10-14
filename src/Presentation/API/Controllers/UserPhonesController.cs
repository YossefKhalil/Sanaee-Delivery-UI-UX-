using Core.Application.Features.UserPhones.Commands.DeleteUserPhone;
using Core.Application.Features.UserPhones.Commands.VerifyPhoneOtp;
using Core.Application.Features.UserPhones.Commands.SendPhoneOtp;
using Core.Application.Features.UserPhones.Dto;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Core.Application.Features.UserPhones.Queries.GetUserPhones;
using Core.Application.Features.UserPhones.Queries.GetDefaultPhone;
using Core.Application.Features.UserPhones.Commands.SetDefaultPhone;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserPhonesController : ControllerBase
    {
        private readonly ISender _mediator;
        public UserPhonesController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("send-otp")]
        public async Task<IActionResult> SendPhoneOtp([FromBody] SendphoneOtpCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { message = result });
        }


        [HttpPost("verify-otp/{id}")]
        public async Task<IActionResult> VerifyPhoneOtp(int id, [FromBody] VerifyOtpDto dto)
        {
            //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new VerifyPhoneOtpCommand(id, dto.PhoneNumber, dto.Code);

            await _mediator.Send(command);
            return Created
                (
                    string.Empty,
                    new { message = "Phone Number Updated Successfully" }
                );
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPhone(int id, [FromQuery] int PhoneId)
        {
            var command = new DeleteUserPhoneCommand(PhoneId, id);

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<GetUserPhonesDto>>> GetUserPhones(int id)
        {
            var query = new GetUserPhonesQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpGet("default/{id}")]
        public async Task<ActionResult<IReadOnlyList<GetUserPhonesDto>>> GetDefaultPhone(int id)
        {
            var query = new GetDefaultPhoneQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPatch("set-default/{id}")]
        public async Task<ActionResult<IReadOnlyList<GetUserPhonesDto>>> SetDefaultPhone(int id, [FromQuery] int PhoneId)
        {
            var command = new SetDefaultPhoneCommand(PhoneId, id);

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
