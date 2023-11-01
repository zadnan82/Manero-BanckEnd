using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manero_BanckEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginSchema user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Fill in Email and Password");
            } 
            if (await _authService.LoginUser(user))
            {
               // var tokenString = _authService.GenerateTokenString(user);
               // return Ok(tokenString);
                return Ok("ok");
            }
            return BadRequest();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegistrationSchema user)
        {
            if (await _authService.RegisterUser(user))
            {
                return Ok("Successfully Registered");
            }
            return BadRequest("Something went wrong");
        }

        

    }
}
