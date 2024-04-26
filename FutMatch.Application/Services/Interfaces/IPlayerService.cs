using FutMatch.Domain.Requests;
using FutMatch.Domain.Responses;

namespace FutMatch.Application.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerResponse?> CreateAsync(PlayerCreateRequest request);
        Task<List<PlayerResponse>> GetAllAsync();
        Task<PlayerResponse?> GetByIdAsync(long id);
        Task<PlayerResponse> UpdateAsync(long id, PlayerUpdateRequest request);
        Task<bool> DeleteAsync(long id);
    }
}
