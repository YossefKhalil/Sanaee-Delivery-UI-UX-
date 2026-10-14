using Core.Application.Features.Orderes.Commands.CancelOrderByAdmin;
using Core.Application.Features.Orderes.Commands.CancelOrderByCustomer;
using Core.Application.Features.Orderes.Commands.CancelOrderByProvider;
using Core.Application.Features.Orderes.Commands.CompleteOrder;
using Core.Application.Features.Orderes.Commands.CreateOrder;
using Core.Application.Features.Orderes.Commands.InProgressOrder;
using Core.Application.Features.Orderes.Dtos;
using Core.Application.Features.Orderes.Queries.GetAllOrdersByCustomer;
using Core.Application.Features.Orderes.Queries.GetAllOrdersForAdmin;
using Core.Application.Features.Orderes.Queries.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _mediator;

        public OrdersController(ISender mediator)
        {
            _mediator = mediator;
        }



        [HttpPost("{UserId}")]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderDto dto, int UserId)
        {
            var command = new CreatedOrderCommand(dto, UserId);

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderById), new {OrderId = result}, new { OrderId = result, message = "Order Created Successfully." });
        }



        [HttpGet("{OrderId}")]
        public async Task<ActionResult<int>> GetOrderById(int OrderId)
        {
            var query = new GetOrderByIdQuery(OrderId);

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("customer/{CustomerId}")]
        public async Task<ActionResult<IReadOnlyList<GetAllOrdersForCustomerDto>>> GetAllOrdersByCustomer(int CustomerID)
        {
            var query = new GetAllOrdersByCustomerQuery(CustomerID);

            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpPatch("customer-cancel")]
        public async Task<ActionResult> CancelOrderByCustomer([FromBody] CancelOrderByCustomerCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("provider-cancel")]
        public async Task<ActionResult> CancelOrderByProvider([FromBody] CancelOrderByProviderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("admin-cancel")]
        public async Task<ActionResult> CancelOrderByAdmin([FromBody] CancelOrderByAdminCommand command)
        {

            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("inprogress/{OrderId}")]
        public async Task<ActionResult> InProgressOrder( int OrderId)
        {
            var command = new InProgressOrderCommand(OrderId);


            await _mediator.Send(command);
            return NoContent();
        }



        [HttpPatch("complete/{OrderId}")]
        public async Task<ActionResult> CompleteOrder(int OrderId)
        {
            var command = new CompleteOrderCommand(OrderId);

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("admin")]
        public async Task<ActionResult<int>> GetAllOrdersForAdminQuery([FromQuery] GetAllOrdersForAdminQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
