namespace Manero_BanckEnd.Entities;

public class ProfileEntity
{
    public int Id { get; set; } 
    public int UserId { get; set; }
    public UserEntity User { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string PhoneNumber { get; set; }  = null !;
}
