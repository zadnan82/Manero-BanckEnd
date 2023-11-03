namespace Manero_BanckEnd.Entities
{
    public class OrderItemsEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public OrderEntity Order { get; set; } = null!;

        public string  ArticleNumberId { get; set; } = null!;

        public ProductEntity Product { get; set; } = null!;

        public decimal OldPrice { get; set; }

        public decimal NewPrice { get; set; }

        public int Quantity { get; set; }

    }
}
