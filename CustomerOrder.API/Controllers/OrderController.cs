using CustomerOrder.API.Validations;
using CustomerOrder.Application.Interfaces;
using CustomerOrder.Common.Helpers;
using CustomerOrder.DTO;
using FluentValidation.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerOrder.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    [ApiVersion("1")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get Order Details
        /// </summary>
        /// <param name="id">The OrderId, of which, the details are to be fetched</param>
        /// <returns>Detail of the specific order with minimum bin length</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDetailResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Details(string id)
        {
            return Ok(await _orderService.GetOrder(id));
        }

        /// <summary>
        /// Save Order
        /// </summary>
        /// <param name="request">The object containing the order details</param>
        /// <returns>The minimum bin length for the order</returns>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SaveOrder(OrderRequest request)
        {
            var validator = new OrderValidator();
            ValidationResult validationResults = validator.Validate(request);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors.ToValidationMessage());
            }

            return Ok(await _orderService.SaveOrder(request));
        }
    }
}