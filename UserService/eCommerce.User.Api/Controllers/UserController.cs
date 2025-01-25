using eCommerce.User.Core.DTOs;
using eCommerce.User.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest();
            }

            var result = await userService.Login(loginDto);

            if (result == null || !result.IsSuccess)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest();
            }

            var result = await userService.Register(registerDto);

            if (result == null || !result.IsSuccess)
            {
                return BadRequest();
            }
             
            return Ok(result);
        }
    }
}
