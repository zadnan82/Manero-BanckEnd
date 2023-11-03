namespace Manero_BanckEnd.Entities
{
    public class CardEntity
    {
        public int Id { get; set; }

        public string  UserId { get; set; } = null!;

        public UserEntity User { get; set; } = null!;
        public string CardHolderName { get; set; } = null!;
        public int CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVV { get; set; }

        //public static implicit operator CardDto(CardEntity entity) => new CardDto()
        //{
        //    CardHolderName = entity.CardHolderName,
        //    CardNumber = entity.CardNumber,
        //    ExpirationDate = entity.ExpirationDate,
        //    CVV = entity.CVV,
        //};
    }
}
