using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Security.Claims;

[Route("api/User")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;
    private readonly TokenGenerator _tokenGenerator;
    private readonly TokenService _tokenService;

    public AuthController(UserService userService, TokenGenerator tokenGenerator, IConfiguration configuration, TokenService tokenService)
    {
        _userService = userService;
        _configuration = configuration;
        _tokenGenerator = tokenGenerator;
        _tokenService = tokenService;
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginRequest user)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Fill in Email and Password");
            
            var result = await _userService.LoginUserAsync(user);

          // var userId = ((UserLoginResult)result.Result!).User.Id;
                Claim[] claims = new Claim[]
                {
               // new Claim("UserId", userId),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("ApiKey", _configuration["ApiKey"]!)
                };

            //var toekResponse = await _tokenService.GenerateTokenAsync(claims);
            //if (toekResponse == null)
            //{
            //    return Unauthorized();
            //} 
            //return Ok(toekResponse); 

             return result.Status switch
             {
                 ResponseStatusCode.OK => Ok(new ResponseWithToken
                 {
                     Token =  _tokenGenerator.Generate(claims),
                     Result = result
                 }),
                 ResponseStatusCode.UNAUTHORIZED => Unauthorized(result.Message),
                 _ => Problem(result.Message),
             };
    }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Problem();
}
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(UserCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.CreateUserAsync(request);

            //var userId = ((UserCreateResult)result.Result!).User.Id;
            Claim[] claims = new Claim[]
            {
               // new Claim("UserId", userId),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Name, request.Email),
                new Claim("ApiKey", _configuration["ApiKey"]!)
                // Add any additional claims needed for authorization
            };

            return result.Status switch
            {
                ResponseStatusCode.CREATED => Created("", new ResponseWithToken
                {
                    Token =  _tokenGenerator.Generate(claims),
                    Result = result
                }),
                ResponseStatusCode.EXIST => Conflict(result.Message),
                _ => Problem(result.Message),
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Problem();
        }
    }



    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh(TokenRequest request)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var tokenResponse = await _tokenService.RefreshTokenAsync(request);
                if (tokenResponse != null)
                    return Ok(tokenResponse);
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return Unauthorized();

    }
}