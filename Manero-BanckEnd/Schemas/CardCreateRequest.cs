using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Schemas;

public class CardCreateRequest
{
    public string CardHolderName { get; set; } = null!;
    public int CardNumber { get; set; }
    public int CVV { get; set; }
    public DateTime ExpirationDate { get; set; }

    public static implicit operator CardEntity(CardCreateRequest request)
    {
        return new CardEntity
        {
            CardHolderName = request.CardHolderName,
            CardNumber = request.CardNumber,
            CVV = request.CVV,
            ExpirationDate = request.ExpirationDate
        };
    }

}
