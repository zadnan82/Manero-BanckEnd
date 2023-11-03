namespace Manero_BanckEnd.Entities
{
    public class FavoritesEntity
    {
        public int Id { get; set; }
        public string  UserId { get; set; } = null!;

        public UserEntity UserEntity { get; set; } = null!;
        public string ArticleNumberId { get; set; } = null!;

        public ProductEntity Product { get; set; } = null!;


    }
}
