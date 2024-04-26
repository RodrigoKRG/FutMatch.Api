namespace FutMatch.Domain.Entities
{
    public abstract class UserProfile : AuditEntity
    {
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public long? AddressId { get; set; }
        public long UserId { get; set; }
        public virtual Address? Address { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
