﻿using Manero_BanckEnd.Models;

namespace Manero_BanckEnd.Schemas
{
    public class UserLoginResult
    {
        public User User { get; set; } = null!;

        public string ApiKey { get; set; } = null!;
    }
}
