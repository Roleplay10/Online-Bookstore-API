using DataBusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Online_Bookstore_API.Controllers
{
    [Authorize]
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("{user_id}/{book_id}")]
        public async Task<IActionResult> addCartItems(int user_id, int book_id)
        {
            if (user_id == 0 || book_id == 0)
            {
                return BadRequest("Incorrect data");
            }
            var state = await _cartService.AddBooks(user_id, book_id);
            return state? Ok("Item added succesfully") : NotFound("User or book not found");
        }
        [HttpGet("{user_id}")]
        public IActionResult getCartItems(int user_id)
        {
            if(user_id == 0)
            {
                return BadRequest("Invalid id");
            }
            var result = _cartService.viewCart(user_id);
            if(result is null)
            {
                return StatusCode(404, "User not found");
            }
            return Ok(result);
        }
        [HttpDelete("{user_id}/{book_id}")]
        public async Task<IActionResult> DeleteBook(int user_id, int book_id)
        {
            if(user_id is 0 || book_id is 0)
            {
                return BadRequest("Invalid id");
            }
            var result = await _cartService.DeleteBook(user_id, book_id);
            return result ? Ok("Deleted succesfully") : NotFound("User or book not found");
        }
    }
}
