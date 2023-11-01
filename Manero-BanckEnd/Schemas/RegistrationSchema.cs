namespace Manero_BanckEnd.Schemas
{
    public class RegistrationSchema
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string? StreetName { get; set; }
        public string? Zipcode { get; set; }
        public string? City { get; set; }

    }
}

