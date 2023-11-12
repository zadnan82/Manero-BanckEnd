using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

[Route("api/User")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;
    private readonly TokenGenerator _tokenGenerator;

    public AuthController(UserService userService, TokenGenerator tokenGenerator, IConfiguration configuration)
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

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("ApiKey", _configuration["ApiKey"]!)
                // Add any additional claims needed for authorization
            };

            return result.Status switch
            {
                ResponseStatusCode.OK => Ok(new ResponseWithToken
                {
                    Token = _tokenGenerator.Generate(claims),
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

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Name, request.Email),
                new Claim("ApiKey", _configuration["ApiKey"]!)
                // Add any additional claims needed for authorization
            };

            return result.Status switch
            {
                ResponseStatusCode.CREATED => Created("", new ResponseWithToken
                {
                    Token = _tokenGenerator.Generate(claims),
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
}
