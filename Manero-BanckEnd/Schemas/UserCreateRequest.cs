﻿namespace Manero_BanckEnd.Schemas
{
    public class UserCreateRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set;} = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

       
    }
}
