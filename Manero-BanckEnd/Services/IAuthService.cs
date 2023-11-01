using Manero_BanckEnd.Schemas;

namespace Manero_BanckEnd.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(LoginSchema user);
        Task<bool> LoginUser(LoginSchema user);
        Task<bool> RegisterUser(RegistrationSchema user);
   
    }
}