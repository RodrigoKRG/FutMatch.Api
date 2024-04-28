using FutMatch.Application.Services.Interfaces;
using FutMatch.Domain.Common.Exceptions;
using FutMatch.Domain.Common.Handlers;
using FutMatch.Domain.Entities;
using FutMatch.Domain.Helpers;
using FutMatch.Domain.Repositories;
using FutMatch.Domain.Requests;
using FutMatch.Domain.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InvalidDataException = FutMatch.Domain.Common.Exceptions.InvalidDataException;


namespace FutMatch.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, IUserRepository userRepository, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<LoginResponse> GenerateToken(LoginRequest request, bool isRefreshToken = false)
        {
            var user = await GetUserByLogin(request.Login);
            var expirationToken = DateTime.Now.AddMinutes(30);
            var expirationRefreshToken = DateTime.Now.AddDays(1);

            if (!isRefreshToken)
                ValidateLogin(request, user);

            if (!user.Active)
            {
                _logger.LogError($"User with email {request.Login} is not active.");
                throw ExceptionHandler.CreateException<InvalidDataException>(message: "Usuário inativo.", _logger);
            }

            var token = GenerateJwtToken(user, expirationToken, true);
            var refreshToken = GenerateJwtToken(user, expirationRefreshToken, false);

            return new LoginResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                TokenExpiration = expirationToken,
                RefreshTokenExpiration = expirationRefreshToken
            };
        }

        private void ValidateLogin(LoginRequest request, User user)
        {
            var hashPassword = PasswordHashHelper.GenerateHashPassword(request.Password, user.Salt);
            if (!hashPassword.Equals(user.Password))
            {
                _logger.LogError($"Password Invalid");
                throw ExceptionHandler.CreateException<InvalidDataException>(
                    message: "Email ou senha invalidos.",
                    _logger
                );
            }
        }

        private async Task<User> GetUserByLogin(string login)
        {
            var user = await _userRepository.GetByLoginAsync(login);
            if (user is null)
            {
                _logger.LogError($"User with login {login} not found.");
                throw ExceptionHandler.CreateException<EntityNotFoundException>(
                    message: "Usuario não cadastrado.",
                    _logger
                );
            }

            return user;
        }

        private string GenerateJwtToken(User user, DateTime expiration, bool addUserClaims)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: GenerateClaims(user, addUserClaims),
                    expires: expiration,
                    signingCredentials: credentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }

        private static List<Claim> GenerateClaims(User user, bool addUserClaims)
        {
            var claims = new List<Claim>
            {
                new Claim(type: "Login", value: user.Login)
            };

            if (addUserClaims)
            {
                claims.AddRange(
                [
                    new Claim(type: "Id", value: user.Id.ToString()),
                    new Claim(type: ClaimTypes.Role, value: user.Role.ToString())
                ]);
            }

            return claims;
        }

        public string GetLogin(ClaimsPrincipal user)
            => user.FindFirst("Login")?.Value!;

        public long GetId(ClaimsPrincipal user)
            =>  long.Parse(user.FindFirst("Id")?.Value!);

    }
}
