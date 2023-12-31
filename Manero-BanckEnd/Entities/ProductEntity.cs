﻿using Manero_BanckEnd.Schemas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Manero_BanckEnd.Entities;

public class ProductEntity
{

    [Key]
    public string ArticleNumber { get; set; } = null!;
    // public int Id { get; set; }
    public string Category { get; set; } = null!;
    //public CategoryEntity Category { get; set; } = null!;

    [Required]
    [Column(TypeName ="nvarchar(450)")]
    public string Name { get; set; } = null!;
    public ICollection<SizeEntity> Size { get; set; } = new List<SizeEntity>();
    public ICollection<ColorEntity> Color { get; set; } = new List<ColorEntity>();

    [Column(TypeName ="money")]
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }

    //public DateTime Created {  get; set; } = DateTime.Now;
    //public DateTime Updated { get; set; } = DateTime.Now;
    public ProductType ProductType { get; set; }
    public bool IsFeatured { get; set; }
    public string ImageLink { get; set; }

    public static implicit operator ProductEntity(ProductCreateRequest request )
    {
        try 
        {
            return new ProductEntity
            {
                ArticleNumber = request.ArticleNumber,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ProductType = request.ProductType,
                Category = request.Category,
                Size = request.Size,
                Color = request.Color,
                Quantity = request.Quantity,
                IsFeatured = request.IsFeatured,
                ImageLink = request.ImageLink,
                  

            };
        }
        catch (Exception e) { Debug.WriteLine(e.Message); }
        return null!; 
    }
}


public enum ProductType
{
    Shirts, Pants, Accessories, Shoes
}