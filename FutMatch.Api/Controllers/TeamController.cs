using FutMatch.Application.Services.Interfaces;
using FutMatch.Domain.Requests;
using FutMatch.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutMatch.Api.Controllers;

[Route("api/teams")]
[ApiController]
[Authorize]
public class TeamController : BaseController
{
    private readonly ITeamService _teamService;
    private readonly ITokenService _tokenService;

    public TeamController(ITeamService teamService, ITokenService tokenService)
    {
        _teamService = teamService;
        _tokenService = tokenService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<TeamResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] TeamCreateRequest request)
    {
        if (!request.IsValid())
            return HandleValidationErrors(request.ValidationResult!);

        var userLogin = _tokenService.GetLogin(User);
        request.SetUser(userLogin);

        var userId = _tokenService.GetId(User);
        request.SetUserId(userId);

        var response = await _teamService.CreateTeamAsync(request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(long id)
    {
        await _teamService.DeleteTeamAsync(id);
        return Ok();
    }

    [HttpGet("{id}/players")]
    [ProducesResponseType(typeof(List<PlayerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlayers(long id)
    {
        var response = await _teamService.GetPlayersAsync(id);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TeamResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var response = await _teamService.GetTeamsAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TeamResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await _teamService.GetTeamAsync(id);
        return Ok(response);
    }
}
