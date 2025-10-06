using Identity.Application.DTOs;
using LoginRequest = Identity.Application.DTOs.LoginRequest;

namespace Identity.Application.Contracts
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    }
}
