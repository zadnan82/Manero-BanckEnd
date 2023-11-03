namespace Manero_BanckEnd.Entities;


public class OrderEntity
{

    public int Id { get; set; }

    public string  UserId { get; set; } = null!;

    public UserEntity User { get; set; } = null!;

    public int PromoCodeId { get; set; }  
    public PromoCodeEntity  PromoCode { get; set; } = null!;

    public int AddressId { get; set; }

    public AddressEntity Address { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public decimal SubTotal { get; set; }

    public decimal Discount { get; set; }

    public decimal Total { get; set; }

    public DateTime Created { get; set; }

    //public static implicit operator OrderDto(OrderEntity entity) => new OrderDto()
    //{
    //    PaymentMethod = entity.PaymentMethod,
    //    SubTotal = entity.SubTotal,
    //    Discount = entity.Discount,
    //    Total = entity.Total,
    //    Created = entity.Created,
    //};


}
