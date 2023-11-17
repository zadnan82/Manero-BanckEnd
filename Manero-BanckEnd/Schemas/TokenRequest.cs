using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Schemas
{
    public class TokenRequest
    {
        public string UserId { get; set; } = null!;

        public UserEntity User { get; set; } = null!; 

        public string AccessToken { get; set; } = null!; 

        public string RefreshToken {  get; set; } = null!;

    }
}
