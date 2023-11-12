using Manero_BanckEnd.Entities; 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Manero_BanckEnd.Contexts;

public class DataContext : DbContext
{
    public DataContext() 
    {
       
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.Migrate();
    }
   
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<AddressTypeEntity> AddressTypes { get; set; }
    public DbSet<CardEntity> Cards { get; set; }    
    public DbSet<FavoritesEntity> Favorites { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderItemsEntity> OrderItems { get; set; }
    public DbSet<ProfileEntity> Profiles { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<PromoCodeEntity> PromoCodes { get; set; }  
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<KeyEntity> ApiKeys { get; set; }

}
