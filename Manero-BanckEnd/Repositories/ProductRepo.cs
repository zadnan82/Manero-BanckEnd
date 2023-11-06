using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Repositories;

public class ProductRepo : Repo<ProductEntity>
{
    public ProductRepo(DataContext context) : base(context)
    {
    }
}
