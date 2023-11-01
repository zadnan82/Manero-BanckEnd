using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Entities;

public class AddressEntity
{
    
    public int Id { get; set; }
    public string StreetName { get; set; } = null!;
    public string Zipcode { get; set; } = null!;
    public string City { get; set; } = null!;
      

  //  public ICollection<AddressEntity> UserAddresses { get; set; } = new HashSet<AddressEntity>();

}
