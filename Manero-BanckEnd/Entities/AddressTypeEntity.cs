namespace Manero_BanckEnd.Entities;

public class AddressTypeEntity
{
    public int Id { get; set; }
    public int  UserId { get; set; } 
    public UserEntity User { get; set; } 
    public int AddressId { get; set; } 
    public AddressEntity Address { get; set; } 
    public string Title { get; set; } = null!;
}
