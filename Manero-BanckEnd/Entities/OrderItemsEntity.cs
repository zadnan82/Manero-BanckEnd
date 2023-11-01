namespace Manero_BanckEnd.Entities
{
    public class OrderItemsEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public OrderEntity Order { get; set; }

        public int ProductId { get; set; }

        public ProductEntity Product { get; set; }

        public decimal OldPrice { get; set; }

        public decimal NewPrice { get; set; }

        public int Quantity { get; set; }

    }
}
