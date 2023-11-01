namespace Manero_BanckEnd.Entities
{
    public class FavoritesEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public UserEntity UserEntity { get; set; }
        public int ProductId { get; set; }   
        
        public ProductEntity Product { get; set; }


    }
}
