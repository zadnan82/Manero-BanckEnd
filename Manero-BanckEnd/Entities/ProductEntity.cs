using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Entities;

public class ProductEntity
{
   
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; }    
    public string Name { get; set; } = null!;
    public string Size { get; set; } = null!;
    public string Color { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public ProductType ProductType { get; set; }
}


public enum ProductType
{
    Shirts, Pants, Accessories, Shoes
}