namespace Manero_BanckEnd.Schemas;

public class UserLoginRequest
{

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
