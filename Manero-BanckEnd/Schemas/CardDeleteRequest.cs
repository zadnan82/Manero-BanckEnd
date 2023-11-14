namespace Manero_BanckEnd.Schemas;

public class CardDeleteRequest
{
    public int CardId { get; set; }

    public static implicit operator int(CardDeleteRequest request)
    {
        return request.CardId;
    }
}
