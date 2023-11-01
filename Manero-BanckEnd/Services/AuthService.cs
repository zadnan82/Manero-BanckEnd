using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Schemas;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Manero_BanckEnd.Services;

public class AuthService : IAuthService
{
   // private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;
    private readonly DataContext _dataContext;
    public AuthService(  IConfiguration config, DataContext userContext)
    {
       // _userManager = userManager;
        _config = config;
        _dataContext = userContext;
    }

    public async Task<bool> RegisterUser(RegistrationSchema user)
    {

        UserEntity entityUser = user;
        await _dataContext.Users.AddAsync(entityUser);
        var result = await _dataContext.SaveChangesAsync();


        var identityUser = new IdentityUser
        {
            UserName = user.Email,
            Email = user.Email
        };
       // var result = await _userManager.CreateAsync(identityUser, user.Password);
        return result == null ?  false : true;
    }

    public async Task<bool> LoginUser(LoginSchema user)
    {
        var currentUser = await _dataContext.Users
          .FirstOrDefaultAsync(u =>
          u.Email.ToLower() == user.Email.ToLower());

        if (currentUser != null)
        {
            if (currentUser.Password.ToString() == user.Password)
            return true;
        }
     
        //var identityUser = await _userManager.FindByEmailAsync(user.Email); 
        //if (identityUser == null)
        //{
        //    return false;
        //}
        //await _userManager.CheckPasswordAsync(identityUser, user.Password);
        return false;
    }

    public string GenerateTokenString(LoginSchema user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,"User"),
            };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

        var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            issuer: _config.GetSection("Jwt:Issuer").Value,
            audience: _config.GetSection("Jwt:Audience").Value,
            signingCredentials: signingCred);

        string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString;
    }


    
}
