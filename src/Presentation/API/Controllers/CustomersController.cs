using Core.Application.Features.Customers.Commands.CreateCustomer;
using Core.Application.Features.Customers.Queries.GetAllCustomersForAdmin;
using Core.Application.Features.Customers.Queries.GetCustomerById;
using Core.Application.Features.Customers.Dtos;
using Core.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ISender _mediator;

        public CustomersController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var userId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetCustomerById), new { id = userId }, new { id = userId , message = "Customer created successfully."});
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetCustomerById(int id)
        {
            var query = await _mediator.Send(new GetCustomerByIdQuery(id));
            return Ok(query);
        }



        [HttpGet("admin")]
        public async Task<ActionResult<PagedResult<UserDto>>> GetAllCustomers([FromQuery] GetAllCustomersForAdminQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
