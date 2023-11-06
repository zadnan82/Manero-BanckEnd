using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Schemas;
using System.Diagnostics;

namespace Manero_BanckEnd.Models
{
    public class Product
    {

        public string ArticleNumber { get; set; } = null!;

        public string Category { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string Color { get; set; } = null!;

        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }

        //public DateTime Created {  get; set; } = DateTime.Now;
        //public DateTime Updated { get; set; } = DateTime.Now;
        public ProductType ProductType { get; set; }

        public static implicit operator Product(ProductEntity entity)
        {
            try
            {
                return new Product
                {
                    ArticleNumber = entity.ArticleNumber,
                    Name = entity.Name,
                    Description = entity.Description,
                    Price = entity.Price,
                    ProductType = entity.ProductType,
                    Size = entity.Size,
                    Color = entity.Color,
                    Quantity = entity.Quantity, 
                    Category = entity.Category
                      
                };
            }
            catch (Exception e) { Debug.WriteLine(e.Message); }
            return null!;
        }
    }
}

