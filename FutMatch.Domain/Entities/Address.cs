namespace FutMatch.Domain.Entities
{
    public class Address : AuditEntity
    {
        public string Street { get; set; } = null!;
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
