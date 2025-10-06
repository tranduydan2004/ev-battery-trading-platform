using Identity.Application.Contracts;
using Identity.Application.DTOs;
using Identity.Domain.Abtractions;

namespace Identity.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(IUserRepository userRepo, IJwtProvider jwtProvider)
        {
            _userRepo = userRepo;
            _jwtProvider = jwtProvider;
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null) return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.UserPassword);
            if (!validPassword) return null;

            var token = _jwtProvider.GenerateToken(user);

            return new LoginResponse
            {
                Token = token,
                ExpireAt = DateTime.UtcNow.AddMinutes(60)
            };
        }
    }
}
