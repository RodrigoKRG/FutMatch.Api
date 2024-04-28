using FutMatch.Domain.Requests;

namespace FutMatch.Domain.Entities
{
    public class Team : AuditEntity
    {
        public string Name { get; set; } = null!;
        public string? Shield { get; set; }
        public virtual List<Player> Players { get; set; } = new();

        public static Team Build(TeamCreateRequest request)
        {
            var team = new Team
            {
                Name = request.Name!,
                Shield = request.Shield,
            };
            team.SetAudit(request.user);
            return team;
        }

        public void Update(TeamUpdateRequest request)
        {
            Name = request.Name!;
            Shield = request.Shield;
            SetAudit(request.User);
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }
    }
}
