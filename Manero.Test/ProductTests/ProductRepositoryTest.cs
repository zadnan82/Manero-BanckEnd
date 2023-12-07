using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test.ProductTests
{
    public class ProductRepositoryTest
    {
        private DataContext DataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new DataContext(options);
        }

        [Fact]
        public async Task CreateProductsAsync_Should_ReturnProductEntity()
        {
            // Arrange
            var context = DataContext();
            var repository = new ProductRepo(context);
            var productRequest = new ProductCreateRequest
            {
                ArticleNumber = "789",
                Name = "Test Product",
                Description = "This is a test product",
                Price = 29.99m,
                ProductType = ProductType.Shirts,
                Category = "Clothing",
                Size = new List<SizeEntity>(),
                Color = new List<ColorEntity>(),
                Quantity = 5,
                IsFeatured = true,
                ImageLink = "https://example.com/test.jpg"
            };

            // Act
            var result = await repository.CreateProductsAsync(productRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductEntity>(result);
            Assert.Equal(productRequest.ArticleNumber, result.ArticleNumber);
            Assert.Equal(productRequest.Name, result.Name);
            Assert.Equal(productRequest.Description, result.Description);
            Assert.Equal(productRequest.Price, result.Price);
            Assert.Equal(productRequest.ProductType, result.ProductType);
            Assert.Equal(productRequest.Category, result.Category);
        }

    }
}
