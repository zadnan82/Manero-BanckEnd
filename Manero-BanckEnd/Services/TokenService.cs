using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Manero_BanckEnd.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;
    private readonly TokenRepo _tokenRepo;

    public TokenService(IConfiguration configuration, TokenRepo tokenRepo)
    {
        _configuration = configuration;
        _tokenRepo = tokenRepo;
    }

    public async Task<string> GenerateRefreshToken(string userId)
    {
        try
        {
            var random = new byte[32]; 
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            var refreshToken = new TokenEntity
            {
                UserId = userId,
                Token = Convert.ToBase64String(random),
                ExpiresAt = DateTime.UtcNow.AddMonths(Convert.ToInt32(_configuration["RefreshToken:ExpiresMonth"]))
            }; 
            var result = await _tokenRepo.SetRefreshTokenAsync(refreshToken);
            return result.Token ??= null!; 
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!; 
    }

    public string GenerateAccessToken(Claim[] claims , int expiresInMinutes = 15)
    {
        try
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]!));
            var signingKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var securityToken = new JwtSecurityToken
            (
                _configuration["Token:Issuer"],
                _configuration["Token:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(expiresInMinutes), // Default expiration time is 30 minutes
                signingCredentials: signingKey
            );

            var Accesstoken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return Accesstoken;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public ClaimsPrincipal ValidateAccessToken(string accessToken)

    {
        try
        {
           var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Token:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]!)),
                ValidateAudience = true,
                ValidAudience = _configuration["Token:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            var principal = tokenHandler.ValidateToken(accessToken, validationParameters, out _);
            return principal; 
             
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;

    }

    public async Task<bool> ValidateRefreshToken (string userId, string refreshToken)
    {
        try
        {
            var result = await _tokenRepo.GetRefreshTokenAsync(userId);
            if (result != null) 
                return result.Token == refreshToken && result.ExpiresAt > DateTime.UtcNow;  
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;

    }

    public async Task<TokenReponse> GenerateTokenAsync(Claim[] claims)
    {

        try
        {
            var tokenResponse = new TokenReponse
            {
                AccessToken = GenerateAccessToken(claims),
                RefreshToken = await GenerateRefreshToken(claims.FirstOrDefault(x => x.Type == "UserId")?.Value ?? null!)
            }; 
            return tokenResponse;
            
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;

    }

    public async Task<TokenReponse> RefreshTokenAsync (TokenRequest request)
    {
        try
        {
            var principal = ValidateAccessToken(request.AccessToken);
            if (principal != null)
            {
                var tokens = await GenerateTokenAsync(principal.Claims.ToArray()); 
                return tokens; 
            }
            var result = await ValidateRefreshToken(request.UserId, request.RefreshToken);
            if (result)
            {
                var tokens = await GenerateTokenAsync([new Claim("UserId", request.UserId)]);
                return tokens;
            } 
        }
        catch (Exception ex ) { Debug.WriteLine(ex.Message); }
        return null!; 
    }


}
