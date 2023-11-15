using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Manero_BanckEnd.Contexts
{
    public class DataInitializer
    {
        private readonly DataContext _dbContext;

        public DataInitializer(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void MigrateData()
        {
            _dbContext.Database.Migrate();
            SeedData();
            _dbContext.SaveChanges();
        }

        private void SeedData()
        {
            if (!_dbContext.Products
            .Any(e => e.Name == "T-Shirt"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "TS-01",
                    Name = "T-Shirt",
                    Category="BestSeller",
                    Size = "M",
                    Color = "White",
                    Price = 150,
                    Description = "A comfortable T-shirt made of organic cotton.",
                    Quantity = 100,
                    ProductType = ProductType.Shirts,
                    IsFeatured = false
                });
            }
            if (!_dbContext.Products
            .Any(e => e.Name == "Jeans"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "JN-01",
                    Name = "Jeans",
                    Category="Featured",
                    Size = "32/32",
                    Color = "Blue",
                    Price = 349,
                    Description = "Classic denim jeans.",
                    Quantity = 50,
                    ProductType = ProductType.Pants,
                    IsFeatured = true
                });
            }
            if (!_dbContext.Products
            .Any(e => e.Name == "Dress Shirt"))
            {
                _dbContext.Add(new ProductEntity
                {
                    ArticleNumber = "DS-01",
                    Name = "Dress Shirt",
                    Category="BestSeller",
                    Size = "L",
                    Color = "Light Blue",
                    Price = 200,
                    Description = "A formal dress shirt.",
                    Quantity = 75,
                    ProductType = ProductType.Shirts,
                    IsFeatured = false
                });
            }
            
        }
    }
}
