using Identity.Domain.Entities;

namespace Identity.Domain.Abtractions
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
