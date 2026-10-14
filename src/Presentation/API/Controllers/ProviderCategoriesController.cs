using Core.Application.Features.ProviderCategories.Commands.ChangeYearsOfExperience;
using Core.Application.Features.ProviderCategories.Commands.CreateProviderCategory;
using Core.Application.Features.ProviderCategories.Dtos;
using Core.Application.Features.ProviderCategories.Queries.GetProviderCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderCategoriesController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProviderCategoriesController(ISender mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("admin/{id}")]
        public async Task<ActionResult<GetProviderCategoryDto>> GetProviderCategoryById(int id)
        {
            var query = new GetProviderCategoryByIdQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpPost]
        public async Task<ActionResult> CreateProviderCategory([FromBody] CreateProviderCategoryCommand command)
        {
            var id = await _mediator.Send(command);
            return Created(string.Empty, new {id = id, message = "Provider Category Created Successfully."});
        }



        [HttpPatch]
        public async Task<ActionResult> ChangeYearsOfExperience([FromBody] ChangeYearsOfExperienceCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
