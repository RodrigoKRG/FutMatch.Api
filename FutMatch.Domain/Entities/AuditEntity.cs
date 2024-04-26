namespace FutMatch.Domain.Entities
{
    public class AuditEntity : BaseEntity
    {
        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; } = null!;
        public DateTime? UpdatedAt { get; private set; }
        public string? UpdatedBy { get; private set; }

        public void SetAudit(string user)
        {
            if (Id == 0)
            {
                CreatedAt = DateTime.Now;
                CreatedBy = user;
            }
            else
            {
                UpdatedAt = DateTime.Now;
                UpdatedBy = user;
            }
        }
    }
}
