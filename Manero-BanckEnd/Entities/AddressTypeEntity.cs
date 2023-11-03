namespace Manero_BanckEnd.Entities;

public class AddressTypeEntity
{
    public int Id { get; set; }
    public string   UserId { get; set; } = null!;
    public UserEntity User { get; set; } = null!;
    public int AddressId { get; set; } 
    public AddressEntity Address { get; set; } = null!;
    public string Title { get; set; } = null!;
}
