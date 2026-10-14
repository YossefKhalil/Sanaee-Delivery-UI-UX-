using Core.Application.Features.ProviderDocuments.Commands.ChangeDocumentStatus;
using Core.Application.Features.ProviderDocuments.Queries.GetDocumentById;
using Core.Application.Features.ProviderDocuments.Commands.CreateDocument;
using Core.Application.Features.ProviderDocuments.Dtos;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Core.Application.Features.ProviderDocuments.Queries.GetAllProviderDocuments;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderDocumentsController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProviderDocumentsController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("{id}")]
        public async Task<ActionResult<int>> CreateDocument([FromBody] ProviderDocumentDto dto, int id)
        {
            var command = new CreateDocumentCommand(dto, id);

            var result = await _mediator.Send(command);
            return CreatedAtAction(
                nameof(GetDocumentById),
                new { id = result },
                new { id = result, message = "Document Sent successfully" });
        }



        [HttpPatch]
        public async Task<ActionResult> ChangeDocumentStatus([FromBody] ChangeDocumentStatusCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        // return Document By it's id for admin to show and check it
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDocumentDto>> GetDocumentById(int id)
        {
            var query = new GetDocumentByIdQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetDocumentDto>>> GetAllProviderDocuments(int id)
        {
            var query = new GetAllProviderDocumentsQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
