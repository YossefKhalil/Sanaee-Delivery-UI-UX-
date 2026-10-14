using Core.Application.Features.Messages.Commands.CreateLocation;
using Core.Application.Features.Messages.Commands.CreateMedia;
using Core.Application.Features.Messages.Commands.CreateText;
using Core.Application.Features.Messages.Commands.CreateVoiceNote;
using Core.Application.Features.Messages.Commands.MarkAsRead;
using Core.Application.Features.Messages.Dtos;
using Core.Application.Features.Messages.Queries.GetMessageById;
using Core.Application.Features.Messages.Queries.GetOrderMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ISender _mediator;

        public MessagesController(ISender mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<GetMessageDto>> GetMessageById(int id)
        {
            var query = new GetMessageByIdQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetMessageDto>>> GetOrderMessages([FromQuery] int OrderId)
        {
            var query = new GetOrderMessagesQuery(OrderId);

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPost("location")]
        public async Task<ActionResult<int>> CreateLocation([FromBody] CreateLocationCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetMessageById), new {id = result}, new {id = result, message = "Message Created Successfully!"});
        }


        [HttpPost("media")]
        public async Task<ActionResult<int>> CreateMedia([FromBody] CreateMediaCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetMessageById), new { id = result }, new { id = result, message = "Message Created Successfully!" });
        }


        [HttpPost("text")]
        public async Task<ActionResult<int>> CreateText([FromBody] CreateTextCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetMessageById), new { id = result }, new { id = result, message = "Message Created Successfully!" });
        }


        [HttpPost("voice")]
        public async Task<ActionResult<int>> CreateVoiceNote([FromBody] CreateVoiceNoteCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetMessageById), new { id = result }, new { id = result, message = "Message Created Successfully!" });
        }



        [HttpPatch]
        public async Task<ActionResult> MarkAsRead([FromBody] MarkAsReadCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
