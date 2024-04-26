namespace FutMatch.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Shield { get; set; }
        public virtual List<Player> Players { get; set; } = new();
    }
}
