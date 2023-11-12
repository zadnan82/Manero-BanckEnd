using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Repositories
{
    public class ProfileRepo : Repo<ProfileEntity>
    {
        public ProfileRepo(DataContext context) : base(context)
        {
        }

    }
}
