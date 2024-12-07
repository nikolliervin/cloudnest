using CloudNest.Api.Models.Dtos;
using CloudNest.Api.Models;

namespace CloudNest.Api.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<RegisterDto>> RegisterAsync(RegisterDto registerDto);
        Task<ApiResponse<JwtLogin>> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
    }
}
