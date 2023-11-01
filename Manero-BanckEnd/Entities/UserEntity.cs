using Manero_BanckEnd.Schemas;
using System.Security.Cryptography;
using System.Text;

namespace Manero_BanckEnd.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] Password { get; private set; } = null!;
        public byte[] SecurityKey { get; private set; } = null!;

        public static implicit operator UserEntity(RegistrationSchema form)
        {
            var userEntity = new UserEntity()
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email.ToLower(),

            };
            userEntity.GenerateSecurPassword(form.Password);
            return userEntity;
        }

        public void GenerateSecurPassword(string password)
        {
            using var hmac = new HMACSHA512();
            SecurityKey = hmac.Key;
            Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); 

        }

    }
}
