using Core.Application.Features.Categories.Commands.DeactivateCategory;
using Core.Application.Features.Categories.Commands.ActivateCategory;
using Core.Application.Features.Categories.Commands.RestoreCategory;
using Core.Application.Features.Categories.Queries.GetAllCategories;
using Core.Application.Features.Categories.Commands.CreateCategory;
using Core.Application.Features.Categories.Commands.DeleteCategory;
using Core.Application.Features.Categories.Commands.UpdateCategory;
using Core.Application.Features.Categories.Queries.GetCategoryById;
using Core.Application.Features.Categories.Dtos;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ISender _mediator;

        public CategoriesController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<int>> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            int CategoryId = await _mediator.Send(command);
            return CreatedAtAction
                   (
                        nameof(GetCategoryById),
                        new {id = CategoryId},
                        new {id = CategoryId, message = "Category created successfully."}
                   );
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryDto dto)
        {
            var command = new UpdateCategoryCommand(id, dto);

            await _mediator.Send(command);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var command = new DeleteCategoryCommand(id);

            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("{id}")]
        public async Task<ActionResult> RestoreCategory(int id)
        {
            var command = new RestoreCategoryCommand(id);

            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("activate/{id}")]
        public async Task<ActionResult> ActivateCategory(int id)
        {
            var command = new ActivateCategoryCommand(id);

            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("deactivate/{id}")]
        public async Task<ActionResult> DeactivateCategory(int id)
        {
            var command = new DeactivateCategoryCommand(id);

            await _mediator.Send(command);
            return NoContent();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(int id)
        {
            var query = new GetCategoryByIdQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
