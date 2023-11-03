namespace Manero_BanckEnd.Helpers
{
    public class ServiceResponse
    {

        public ResponseStatusCode Status { get; set; } = ResponseStatusCode.OK;
        public string? Message { get; set; }
        public object? Result { get; set; }
    }
}
