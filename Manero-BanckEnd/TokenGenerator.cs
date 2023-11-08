using Manero_BanckEnd.Models;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Manero_BanckEnd
{
    public class TokenGenerator
    {
         
            private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        { 
            _configuration = configuration;
        }


        public string Generate()
        {

          
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]!));
            var signingKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


            var securityToken = new JwtSecurityToken
                (
                    _configuration["Token:Issuer"],
                    _configuration["Token:Audience"],
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingKey
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public string Generate(Claim[] claims )
        { 

            //var claims = new Claim[]
            //    {
            //            new Claim(ClaimTypes.Email, user.Email),
            //            new Claim(ClaimTypes.Name, user.Email),
            //            new Claim("ApiKey", _configuration["ApiKeys:User"]!)
            //    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]!));
            var signingKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var securityToken = new JwtSecurityToken
                (
                    _configuration["Token:Issuer"],
                    _configuration["Token:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingKey
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
