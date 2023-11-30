namespace Manero_BanckEnd.Models;

public class PromoCodeModel
{
    public int Id { get; set; }

    public string PromoName { get; set; } = null!;

    public double SalePercentage { get; set; }

    public DateTime Validity { get; set; }
}
