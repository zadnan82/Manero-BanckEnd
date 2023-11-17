namespace Manero_BanckEnd.Models;

public class CardModel
{
    public int Id { get; set; }
    public string CardHolderName { get; set; } = null!;
    public int CardNumber { get; set; }

    public int CVV { get; set; }

    public DateTime ExpirationDate { get; set; }
}
