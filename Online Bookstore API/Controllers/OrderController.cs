using DataBusinessLogic.DTOs.OrderDTOs;
using DataBusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Online_Bookstore_API.Controllers
{
    [Authorize]
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("{user_id}")]
        public async Task<IActionResult> placeOrder(int user_id, [FromBody] List<OrderQuantityDTO> qty)
        {
            if (!ModelState.IsValid || user_id == 0)
            {
                return BadRequest("Invalid data");
            }
            var result = await _orderService.PlaceOrder(user_id, qty);
            return result ? Ok("Order created succesfully") : BadRequest("Error creating order");
        }
        [HttpGet("{user_id}")]
        public IActionResult getUserOrders(int user_id)
        {
            var result = _orderService.GetUserOrders(user_id);
            return (result is not null) ? Ok(result) : BadRequest("Invalid id");
        }
    }
}
