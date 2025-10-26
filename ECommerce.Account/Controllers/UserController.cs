using Entities.Domain_Models;
using Entities.Dto;
using Entities.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Service.Implementations;
using Service.Interfaces;
using System.Threading.Tasks;

namespace ECommerce.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterDto userDto)
        {
            if (userDto == null)
                return BadRequest(new { message = "User data is required." });

            try
            {
                var createdUserDto = await _userService.RegisterUserAsync(userDto);
       
                return Created(
                    $"/api/User/email/{createdUserDto.Email}",
                    new
                    {
                        StatusCode = 201,
                        message = "User registered successfully.",
                        data = createdUserDto
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

      
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest(new { message = "Login data is required." });

            var user = await _userService.LoginAsync(loginDto.Email, loginDto.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password." });

            return Ok(new
            {
                StatusCode = 200,
                message = "Login successful.",
                data = user
            });
        }

        
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest(new { message = "Email is required." });

            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null)
                return NotFound(new { message = "User not found." });

            var responseDto = new UserResponseDto
            {
                UniqueKey = user.UserGuid,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(new
            {
                StatusCode = 200,
                message = "User retrieved successfully.",
                data = responseDto
            });
        }
    }
}
