using Manero_BanckEnd.Models;

namespace Manero_BanckEnd.Schemas;

public class UserCreateResult
{

    public User User {  get; set; } = null!;

    public string ApiKey { get; set; } = null!;
}
