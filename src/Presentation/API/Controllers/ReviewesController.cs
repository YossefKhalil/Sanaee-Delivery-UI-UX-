using Application.Features.Reviewes.Dto;
using Core.Application.Features.Reviewes.Commands.CreateReview;
using Core.Application.Features.Reviewes.Commands.DeleteReviewByAdmin;
using Core.Application.Features.Reviewes.Commands.UpdateReview;
using Core.Application.Features.Reviewes.Dto;
using Core.Application.Features.Reviewes.Queries.GetReviewById;
using Core.Application.Features.Reviewes.Queries.GetReviewsByRevieweeId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewesController : ControllerBase
    {
        private readonly ISender _mediator;

        public ReviewesController(ISender mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetReviewByIdDto>> GetReviewById(int id)
        {
            var query = new GetReviewByIdQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetReviewsByRevieweeIdDto>>> GetReviewesByRevieweeId(int RevieweeId)
        {
            var query = new GetReviewsByRevieweeIdQuery(RevieweeId);

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpPost]
        public async Task<ActionResult<int>> CreateReview([FromBody] CreateReviewCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetReviewById), new {id = result}, new {id = result, message = "Review Created Successfully."});
        }


        [HttpPut]
        public async Task<ActionResult> UpdateReview([FromBody] UpdateReviewCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletelReviewByAdmin(int id)
        {
            var command = new DeleteReviewByAdminCommand(id);

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
