using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using Ecommerce.Services.Orders.Application.Commands;
using Ecommerce.Services.Orders.Application.Queries;

namespace Ecommerce.Services.Orders.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;

        public OrdersController(ILogger<OrdersController> logger,
			IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetAsync), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var query = new GetOrderQuery(id);
            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("customer/{customerId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCustomerIdAsync(
			[FromRoute] Guid customerId,
			[FromQuery] int page = 0,
			[FromQuery] int size = 10)
        {
            var query = new GetOrdersByCustomerIdQuery(customerId, page, size);
            var result = await _mediator.Send(query);

            return Ok(result);
		}

        [HttpGet]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPageAsync(
			[FromQuery] int page = 0,
			[FromQuery] int size = 10)
        {
            var query = new GetOrdersQuery(page, size);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id,
			[FromBody] UpdateOrderCommand command)
        {
            if (command == null || id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var command = new DeleteOrderCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
