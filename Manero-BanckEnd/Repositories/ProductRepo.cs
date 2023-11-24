using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
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
}
