using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Manero_BanckEnd.Controllers
{

    [Route("api/User")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;

        public AuthController(UserService userService, TokenGenerator tokenGenerator,  IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;   
        }

     

        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Fill in Email and Password");
                }


                var result = await _userService.LoginUserAsync(user);
                return result.Status switch
                {
                    ResponseStatusCode.OK => Ok(new ResponseWithToken
                    {
                        Token = _tokenGenerator.Generate()
                    }),
                    ResponseStatusCode.UNAUTHORIZED => Unauthorized(result.Message),
                    _ => Problem(result.Message),
                };

   


            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Problem();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _userService.CreateUserAsync(request);
                return result.Status switch
                {
                    ResponseStatusCode.CREATED => Created("", result.Result),
                    ResponseStatusCode.EXIST => Conflict( result.Message),
                    _ => Problem(result.Message),
                };
            }
            catch   (Exception ex) { Debug.WriteLine(ex.Message); }
            return Problem();
           
        }

        

    }
}
