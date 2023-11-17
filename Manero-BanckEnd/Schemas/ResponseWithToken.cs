namespace Manero_BanckEnd.Schemas
{
    public class ResponseWithToken
    {
        // public TokenReponse Token { get; set; } = null!;

        public string  Token { get; set; } = null!;
        public object? Result { get; set; }
    }
}
