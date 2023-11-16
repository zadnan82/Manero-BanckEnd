using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Schemas
{
    public class ProductCreateRequest
    {
        public string ArticleNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string Color { get; set; } = null!;

        public string Category { get; set; } = null!; 
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public ProductType ProductType { get; set; }
        public bool IsFeatured { get; set; }
        public string ImageLink { get; set; }

    }
}
