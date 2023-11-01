namespace Manero_BanckEnd.Entities
{
    public class PromoCodeEntity
    {

        public int Id { get; set; }
        public string PromoName { get; set; } = null!;

        public double SalePercentage { get; set; }

        public DateTime Validity { get; set; }

        //public static implicit operator PromoDto(PromoEntity entity) => new PromoEntity()
        //{
        //    PromoName = entity.PromoName,
        //    SalePercentage = entity.SalePercentage,
        //    Validity = entity.Validity
        //};


    }
}
