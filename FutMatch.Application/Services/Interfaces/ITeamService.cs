using FutMatch.Domain.Requests;
using FutMatch.Domain.Responses;

namespace FutMatch.Application.Services.Interfaces;

public interface ITeamService
{
    Task<TeamResponse> CreateTeamAsync(TeamCreateRequest request);
    Task<TeamResponse> UpdateTeamAsync(long id, TeamUpdateRequest request);
    Task<TeamResponse> GetTeamAsync(long id);
    Task<List<TeamResponse>> GetTeamsAsync();
    Task DeleteTeamAsync(long id);
    Task<List<PlayerResponse>> GetPlayersAsync(long id);
}
