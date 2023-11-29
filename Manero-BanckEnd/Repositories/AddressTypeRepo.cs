using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Repositories
{
    public class AddressTypeRepo : Repo<AddressTypeEntity>
    {
        public AddressTypeRepo(DataContext context) : base(context)
        {
        }

    }
}
