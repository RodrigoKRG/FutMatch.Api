namespace FutMatch.Domain.Entities
{
    public class FieldPosition : BaseEntity
    {
        public string Name { get; set; } = null!;
        public virtual List<Player>? Players { get; set; }
    }
}
