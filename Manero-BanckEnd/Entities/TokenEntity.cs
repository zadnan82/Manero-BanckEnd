using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Entities
{
    public class TokenEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 

        public string UserId { get; set; } = null!;

        public UserEntity User { get; set; } = null!; 

        public string Token { get; set; } = null!;

        public DateTime ExpiresAt { get; set; } 

        public bool IsRevoked { get; set; } = false ;
    }
}
