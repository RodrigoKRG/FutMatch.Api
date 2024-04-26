using FutMatch.Application.Services.Interfaces;
using FutMatch.Domain.Common.Exceptions;
using FutMatch.Domain.Common.Handlers;
using FutMatch.Domain.Entities;
using FutMatch.Domain.Helpers;
using FutMatch.Domain.Repositories;
using FutMatch.Domain.Requests;
using FutMatch.Domain.Responses;
using FutMatch.Domain.Validators;
using Microsoft.Extensions.Logging;
using InvalidDataException = FutMatch.Domain.Common.Exceptions.InvalidDataException;


namespace FutMatch.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ILogger<PlayerService> _logger;
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(ILogger<PlayerService> logger, IPlayerRepository playerRepository)
        {
            _logger = logger;
            _playerRepository = playerRepository;
        }

        public async Task<PlayerResponse?> CreateAsync(PlayerCreateRequest request)
        {
            var validator = new PlayerCreateValidator(_playerRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogError($"User validation failed: {validationResult.Errors}");
                throw ExceptionHandler.CreateException<InvalidDataException>(
                    message: validationResult.Errors.First().ToString(),
                    _logger
                 );
            }
            var player = Player.Build(request);
            
            var salt = PasswordHashHelper.GenerateSalt();
            var hashPassword = PasswordHashHelper.GenerateHashPassword(request.Password, salt);
            var user = User.Build(request.Email, hashPassword, salt, request.User);

            player.SetUser(user);

            var result = await _playerRepository.AddAsync(player);
            return PlayerResponse.Build(result);
        }

        public async Task<PlayerResponse> UpdateAsync(long id, PlayerUpdateRequest request)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player is null)
            {
                _logger.LogError($"User with id {id} not found.");
                throw ExceptionHandler.CreateException<EntityNotFoundException>(
                    message: "Usuário com id {0} não encontrado.",
                    _logger,
                    parameters: new string[] { id.ToString() });
            }

            player.Update(request);
            var result = await _playerRepository.UpdateAsync(player);
            return PlayerResponse.Build(result);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player is null)
            {
                _logger.LogError($"User with id {id} not found.");
                throw ExceptionHandler.CreateException<EntityNotFoundException>(
                    message: "Usuário com id {0} não encontrado.",
                    _logger,
                    parameters: new string[] { id.ToString() }
                );
            }

            return await _playerRepository.RemoveAsync(player);
        }

        public async Task<List<PlayerResponse>> GetAllAsync()
        {
            var users = await _playerRepository.GetAllAsync();
            return users.Select(PlayerResponse.Build).ToList();
        }

        public async Task<PlayerResponse?> GetByIdAsync(long id)
        {
            var user = await _playerRepository.GetByIdAsync(id);
            return user is null ? null : PlayerResponse.Build(user);
        }
    }
}
