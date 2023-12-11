using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test.ProductTests
{
    public class ProductEntityTest
    {
        private readonly ProductCreateRequest productCreateRequest = new ProductCreateRequest
        {
            ArticleNumber = "12345",
            Name = "Test Product",
            Description = "This is a test product",
            Price = 19.99m,
            ProductType = ProductType.Shirts,
            Category = "Clothing",
            Size = new List<SizeEntity>{new SizeEntity { Size = "S" }, new SizeEntity { Size = "M" }, new SizeEntity { Size = "L" }},
            Color = new List<ColorEntity> { new ColorEntity { Color = "Black" }, new ColorEntity { Color = "White" }, new ColorEntity { Color = "Beige" } },
            Quantity = 10,
            IsFeatured = true,
            ImageLink = "https://example.com/image.jpg"
        };

        [Fact]
        public void ProductCreateRequest_Should_Convert_ToProductEntity()
        {
            // Arrange
            var request = productCreateRequest;

            // Act 
            var productEntity = (ProductEntity)request;

            // Assert
            Assert.NotNull(productEntity);
            Assert.IsType<ProductEntity>(productEntity);
            Assert.Equal(request.ArticleNumber, productEntity.ArticleNumber);
            Assert.Equal(request.Name, productEntity.Name);
            Assert.Equal(request.Description, productEntity.Description);
            Assert.Equal(request.Price, productEntity.Price);
            Assert.Equal(request.ProductType, productEntity.ProductType);
            Assert.Equal(request.Category, productEntity.Category);
            Assert.Equal(request.Size, productEntity.Size);
            Assert.Equal(request.Color, productEntity.Color);
            Assert.Equal(request.Quantity, productEntity.Quantity);
            Assert.Equal(request.IsFeatured, productEntity.IsFeatured);
            Assert.Equal(request.ImageLink, productEntity.ImageLink);
        }

    }
}
