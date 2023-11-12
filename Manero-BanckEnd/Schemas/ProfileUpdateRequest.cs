using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Schemas
{
    public class ProfileUpdateRequest
    {
         
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;  
        public string ImageUrl { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}