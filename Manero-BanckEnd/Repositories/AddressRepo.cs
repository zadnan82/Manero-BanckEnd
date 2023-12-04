using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Repositories
{
    public class AddressRepo : Repo<AddressEntity>
    {
        public AddressRepo(DataContext context) : base(context)
        {
        }

    }
}
