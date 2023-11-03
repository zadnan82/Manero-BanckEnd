using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.Json;

namespace Manero_BanckEnd.Repositories
{
    public class ApiKeyRepo : Repo<KeyEntity>
    {
        public ApiKeyRepo(DataContext context) : base(context)
        {
          
        }

        public override Task<KeyEntity> CreateAsync(KeyEntity entity)
        {
            entity.Key = Base64.Encode(JsonSerializer.Serialize(entity));
            return base.CreateAsync(entity);
        }

        
    }
}
