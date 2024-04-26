using FutMatch.Domain.Requests;
using FutMatch.Domain.Responses;
using System.Security.Claims;

namespace FutMatch.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<LoginResponse> GenerateToken(LoginRequest request, bool isRefreshToken = false);
        string GetLogin(ClaimsPrincipal user);
    }
}
