namespace Manero_BanckEnd.Schemas
{
    public class AddressUpdateRequest
    {
        public string StreetName { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Title { get; set; } = null!;
        public int AddressId { get; set; }
    }
}
