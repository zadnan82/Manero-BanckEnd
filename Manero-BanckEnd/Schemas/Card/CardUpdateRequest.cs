using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Schemas.Card;

public class CardUpdateRequest
{
    public int Id { get; set; }

    public string CardNameHolder { get; set; } = null!;

    public int CardNumber { get; set; }

    public int CVV { get; set; }

    public DateTime ExpireDate { get; set; }

    public static implicit operator CardEntity(CardUpdateRequest request)
    {
        return new CardEntity()
        {
            Id = request.Id,
            CardHolderName = request.CardNameHolder,
            CardNumber = request.CardNumber,
            CVV = request.CVV,
            ExpirationDate = request.ExpireDate,
        };
    }
}
