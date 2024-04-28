using FutMatch.Domain.Entities;

namespace FutMatch.Domain.Responses;

public class TeamResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Shield { get; set; } = null!;
    public List<PlayerResponse> Players { get; set; } = null!;

    public static TeamResponse Build(Team entity) =>
        new TeamResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Shield = entity.Shield,
            Players = entity.Players.Select(PlayerResponse.Build).ToList()
        };
}
