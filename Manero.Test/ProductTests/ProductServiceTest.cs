using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test.ProductTests
{
    public class ProductServiceTest
    {
        private DataContext DataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new DataContext(options);
        }

        private readonly ProductCreateRequest productCreateRequest = new ProductCreateRequest
        {
            ArticleNumber = "12345",
            Name = "Test Product",
            Description = "This is a test product",
            Price = 19.99m,
            ProductType = ProductType.Shirts,
            Category = "Clothing",
            Size = new List<SizeEntity> { new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" } },
            Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "White" }, new ColorEntity { Color = "Beige" } },
            Quantity = 10,
            IsFeatured = true,
            ImageLink = "https://example.com/image.jpg"
        };

        [Fact]
        public async Task TakeProductsAsync_Should_Return_ServiceResponse_With_Taken_Products()
        {
            // Arrange
            var context = DataContext();
            var take = 2;
            var productRepository = new ProductRepo(context);
            var productService = new ProductService(productRepository);

            // Seed some products in the in-memory database
            var product1 = new ProductEntity { ArticleNumber = "1", Category = "Clothing", Description = "best", ImageLink = "picture", Name = "shirt" };
            var product2 = new ProductEntity { ArticleNumber = "2", Category = "Clothing", Description = "best", ImageLink = "picture", Name = "shirt" };
            var product3 = new ProductEntity { ArticleNumber = "3", Category = "Electronics", Description = "best", ImageLink = "picture", Name = "shirt" };
            await context.Products.AddRangeAsync(product1, product2, product3);
            await context.SaveChangesAsync();

            // Act
            var result = await productService.TakeProductsAsync(take, "Clothing");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseStatusCode.OK, result.Status);
            Assert.NotNull(result.Result);

            // Updated assertion to handle ListPartition
            Assert.IsAssignableFrom<IEnumerable<ProductEntity>>(result.Result);

            // Convert to List to check Count
            var productList = ((IEnumerable<ProductEntity>)result.Result).ToList();
            Assert.Equal(2, productList.Count);
            Assert.All(productList, product => Assert.Equal("Clothing", product.Category));
        }
    
        
    

        [Fact]
        public async Task CreateProductAsync_Should_Return_ServiceResponse_Created()
        {
            // Arrange
            var context = DataContext();
            var productRepository = new ProductRepo(context);
            var productService = new ProductService(productRepository);

            var productRequest = new ProductCreateRequest
            {
                ArticleNumber = "123",
                Name = "Test Product",
                Description = "This is a test product",
                Price = 19.99m,
                ProductType = ProductType.Shirts,
                Category = "Clothing",
                Size = new List<SizeEntity> { new SizeEntity { Size = "One size" } },
                Color = new List<ColorEntity> { new ColorEntity { Color = "Blue" } },
                Quantity = 10,
                IsFeatured = true,
                ImageLink = "https://example.com/image.jpg"
            };

            // Act
            var result = await productService.CreateProductAsync(productRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseStatusCode.CREATED, result.Status);
            Assert.Equal("Product was crated successfully", result.Message);
            Assert.NotNull(result.Result);
            Assert.IsType<ProductEntity>(result.Result);
            Assert.Equal(productRequest.ArticleNumber, ((ProductEntity)result.Result).ArticleNumber);
            Assert.Equal(productRequest.Name, ((ProductEntity)result.Result).Name);
        }
    }
}
