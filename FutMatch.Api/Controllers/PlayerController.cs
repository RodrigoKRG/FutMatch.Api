using FutMatch.Application.Services.Interfaces;
using FutMatch.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutMatch.Api.Controllers
{
    [Route("api/players")]
    public class PlayerController : BaseController
    {
        private readonly IPlayerService _userService;

        public PlayerController(IPlayerService playerService)
        {
            _userService = playerService;
        }

        [HttpGet]
        [Authorize(Roles = "Master, Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Master, Admin")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Retorna um usuário
        /// </summary>
        /// <response code="200">Retorna uma usuário</response>
        /// <response code="400">Não existe usuário com o id informado</response>
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] PlayerCreateRequest request)
        {
            if (!request.IsValid())
                return HandleValidationErrors(request.ValidationResult);

            request.SetUser(request.Email);

            var response = await _userService.CreateAsync(request);
            return Ok(response);
        }

        [HttpPut("Update")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] PlayerUpdateRequest request)
        {
            if (!request.IsValid())
                return HandleValidationErrors(request.ValidationResult);

            var user = await _userService.UpdateAsync(id, request);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _userService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
