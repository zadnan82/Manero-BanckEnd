using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Repositories;

public class PromoCodeRepo : PromoCodeRepository<PromoCodeEntity>
{
    public PromoCodeRepo(DataContext context) : base(context)
    {
    }
}
