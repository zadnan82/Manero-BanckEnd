namespace Manero_BanckEnd.Entities;

public class ProfileEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public UserEntity User { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string PhoneNumber { get; set; }  = null !;
}
