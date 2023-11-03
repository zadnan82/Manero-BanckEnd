using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Repositories
{
    public class UserRepo : Repo<UserEntity>
    {
        public UserRepo(DataContext context) : base(context)
        {
        }
    }
}
