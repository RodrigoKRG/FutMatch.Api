using FutMatch.Application.Services.Interfaces;
using FutMatch.Domain.Common.Exceptions;
using FutMatch.Domain.Common.Handlers;
using FutMatch.Domain.Entities;
using FutMatch.Domain.Repositories;
using FutMatch.Domain.Requests;
using FutMatch.Domain.Responses;
using Microsoft.Extensions.Logging;

namespace FutMatch.Application.Services;

public class TeamService : ITeamService
{
    private readonly ILogger<TeamService> _logger;
    private readonly ITeamRepository _teamRepository;
    private readonly IPlayerRepository _playerRepository;

    public TeamService(ILogger<TeamService> logger, ITeamRepository teamRepository, IPlayerRepository playerRepository)
    {
        _logger = logger;
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
    }

    public async Task<TeamResponse> CreateTeamAsync(TeamCreateRequest request)
    {
        var team = Team.Build(request);
        var userPlayer = await _playerRepository.GetByUserId(request.UserId);
        team.AddPlayer(userPlayer!);


        foreach (var id in request.PlayerIds)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player is null)
            {
                throw ExceptionHandler.CreateException<EntityNotFoundException>(
                            "Jogador com Id [{0}] não encontrado.",
                           _logger,
                          parameters: new string[] { id.ToString() });
            }

            team.AddPlayer(player);
        }

        var result = await _teamRepository.AddAsync(team);
        var response = TeamResponse.Build(result);

        return response;
    }

    public async Task DeleteTeamAsync(long id)
    {
        var team = await _teamRepository.GetByIdAsync(id);
        if (team is null)
            throw ExceptionHandler.CreateException<EntityNotFoundException>(
                "Usuário com Id [{0}] não encontrado. ",
                _logger,
                parameters: new string[] { id.ToString() }
            );

        await _teamRepository.RemoveAsync(team);

    }

    public async Task<List<PlayerResponse>> GetPlayersAsync(long id)
    {
        var response = new List<PlayerResponse>();
        var players = await _teamRepository.GetPlayers(id);

        foreach (var player in players)
            response.Add(PlayerResponse.Build(player));

        return response;
    }

    public async Task<TeamResponse> GetTeamAsync(long id)
    {
        var team = await _teamRepository.GetByIdAsync(id);

        if (team is null)
            throw ExceptionHandler.CreateException<EntityNotFoundException>(
                "Usuário com Id [{0}] não encontrado. ",
                _logger,
                parameters: new string[] { id.ToString() }
            );

        var response = TeamResponse.Build(team);

        return response;
    }

    public async Task<List<TeamResponse>> GetTeamsAsync()
    {
        var response = new List<TeamResponse>();
        var teams = await _teamRepository.GetAllAsync();

        foreach (var team in teams)
            response.Add(TeamResponse.Build(team));

        return response;
    }

    public async Task<TeamResponse> UpdateTeamAsync(long id, TeamUpdateRequest request)
    {
        var existentTeam = await _teamRepository.GetByIdAsync(id);

        if (existentTeam is null)
            throw ExceptionHandler.CreateException<EntityNotFoundException>(
                               "Team com Id [{0}] não encontrado. ",
                               _logger,
                               parameters: new string[] { id.ToString() }
            );

        existentTeam.Update(request);

        var result = await _teamRepository.UpdateAsync(existentTeam);
        var response = TeamResponse.Build(result);
        return response;
    }
}
