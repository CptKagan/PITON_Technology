using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace PITON_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase{
        private readonly IUserService _userService;
        
        public UserController(IUserService userService){
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] RegisterRequest registerRequest){

            var result = await _userService.CreateAsync(registerRequest);
            if(!result.success){
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest){
            var token = await _userService.LoginAsync(loginRequest);

            if(token == null){
                return Unauthorized(new {message = "Invalid Credentials"});
            }

            return Ok(token);
        }

        [HttpGet("deneme")]
        [Authorize]
        public IActionResult GetSecret(){
            var userid = User.FindFirst("userid")?.Value;

            return Ok(new{
                Message = $"Welcome, your id is: {userid}" 
            });
        }

        [HttpGet("denemeadmin")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetSecretAdmin(){
            var userid = User.FindFirst("userid")?.Value;

            return Ok(new{
                Message = $"Welcome ADMIN!, your id is: {userid}" 
            });
        }
    }
}