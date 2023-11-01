using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Entities;

public class CategoryEntity
{
   
    public int Id { get; set; }
     
    public string Name { get; set; } = null!;
}
