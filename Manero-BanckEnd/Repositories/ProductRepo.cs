using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Manero_BanckEnd.Repositories;

public class ProductRepo : Repo<ProductEntity>
{
    private readonly DataContext _context;
    public ProductRepo(DataContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<ProductEntity>> GetAsync()
    {
        // Include related sizes and colors
        return await _context.Products
            .Include(p => p.Size)
            .Include(p => p.Color)
            .ToListAsync();
    }
    public async Task<ProductEntity> CreateProductsAsync(ProductCreateRequest request)
    {
        // Convert ProductCreateRequest to ProductEntity
        var productEntity = new ProductEntity
        {
            ArticleNumber = request.ArticleNumber,
            Name = request.Name,
            Category = request.Category,
            Size = request.Size.ToList(),
            Color = request.Color.ToList(),
            Price = request.Price,
            Description = request.Description,
            Quantity = request.Quantity,
            ProductType = request.ProductType,
            IsFeatured = request.IsFeatured,
            ImageLink = request.ImageLink
        };

        await _context.Products.AddAsync(productEntity);
        await _context.SaveChangesAsync();

        return productEntity;
    }
    public async Task<ProductEntity> GetByArticleNumberAsync(string articleNumber)
    {
        return await _context.Products
            .Include(p => p.Size)
            .Include(p => p.Color)
            .FirstOrDefaultAsync(p => p.ArticleNumber == articleNumber);
    }

}
