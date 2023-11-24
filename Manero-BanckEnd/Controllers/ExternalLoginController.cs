//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Manero_BanckEnd.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ExternalLoginController : ControllerBase
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly IConfiguration _configuration;

//        public ExternalLoginController(
//            UserManager<IdentityUser> userManager,
//            SignInManager<IdentityUser> signInManager,
//            IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _configuration = configuration;
//        }

//        [HttpGet("ExternalLogin")]
//        public IActionResult ExternalLogin(string provider)
//        {
//            var properties = new AuthenticationProperties
//            {
//                RedirectUri = Url.Action("ExternalLoginCallback"),
//            };

//            return Challenge(properties, provider);
//        }

//        [HttpGet("ExternalLoginCallback")]
//        public async Task<IActionResult> ExternalLoginCallback()
//        {
//            var info = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

//            if (info == null)
//            {
//                return RedirectToAction("ExternalLogin");
//            }

//            var userId = info.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            var email = info.Principal.FindFirst(ClaimTypes.Email)?.Value;
//            var username = info.Principal.FindFirst(ClaimTypes.Name)?.Value;

//            var existingUser = await _userManager.FindByEmailAsync(email);

//            if (existingUser != null)
//            {
//                await _signInManager.SignInAsync(existingUser, isPersistent: false);
//            }
//            else
//            {
//                var newUser = new IdentityUser { UserName = username, Email = email };
//                var result = await _userManager.CreateAsync(newUser);

//                if (result.Succeeded)
//                {
//                    await _signInManager.SignInAsync(newUser, isPersistent: false);
//                }
//                else
//                {
//                    return RedirectToAction("ExternalLogin");
//                }
//            }

//            var token = GenerateToken(userId, email, username);

//            return Ok(new { token });
//        }

//        private string GenerateToken(string userId, string email, string username)
//        {
//            var secretKey = GenerateRandomSecretKey(64);
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//            new Claim(ClaimTypes.Name, username),
//            new Claim(ClaimTypes.Email, email),
//            new Claim(ClaimTypes.NameIdentifier, userId)
//        };

//            var token = new JwtSecurityToken(
//                issuer: "https://localhost:7056/",
//                audience: "https://localhost:7056/",
//                claims: claims,
//                expires: DateTime.Now.AddHours(1),
//                signingCredentials: credentials
//            );

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var tokenString = tokenHandler.WriteToken(token);

//            return tokenString;
//        }

//        static string GenerateRandomSecretKey(int length)
//        {
//            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
//            var random = new Random();
//            var secretKey = new string(Enumerable.Repeat(chars, length)
//                .Select(s => s[random.Next(s.Length)]).ToArray());

//            return secretKey;
//        }
//    }
//}
