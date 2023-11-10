namespace Manero_BanckEnd.Schemas
{
    public class ResponseWithToken
    {
        public string Token { get; set; } = null!;
        public object? Result { get; set; }
    }
}
