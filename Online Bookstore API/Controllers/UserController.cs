using DataBusinessLogic.DTOs.UserDTOs;
using DataBusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Online_Bookstore_API.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserFormDTO register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _userService.registerUser(register);
            if(token == null)
            {
                return BadRequest("Account not created");
            }
            //return new JsonResult(new { token });
            return Ok(new { token });
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] UserFormDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = _userService.loginUser(login);
            if (token == null)
            {
                return BadRequest("Invalid credentials");
            }
            //return new JsonResult(new { token });
            return Ok(new { token });
        }
        [HttpGet("{id}")]
        public IActionResult getUserInfo(int id)
        {
            var info = _userService.getUserInfobyId(id);
            if(info is null)
            {
                return BadRequest("Invalid id");
            }
            return Ok(info);
            //return  new JsonResult(info);
        }
        [HttpPut("{id}")]
        public IActionResult updateUser(int id, [FromBody] UserUpdateDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            var info = _userService.updateUserbyId(id, user);
            if (info is null)
            {
                return NotFound("User not found");
            }
            return Ok("User updated succesfully");
        }

    }
}
