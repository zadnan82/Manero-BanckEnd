namespace Manero_BanckEnd.Schemas;

public class LoginSchema
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    //public bool RememberMe { get; set; } = false;
}
