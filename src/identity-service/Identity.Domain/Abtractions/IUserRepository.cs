using Identity.Domain.Entities;

namespace Identity.Domain.Abtractions
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<User>> SearchAsync(string q, int take = 50, CancellationToken ct = default);
        Task AddAsync(User entity, CancellationToken ct = default);
    }
}
