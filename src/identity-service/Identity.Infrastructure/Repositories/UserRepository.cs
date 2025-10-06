using Identity.Domain.Abtractions;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) { _db = db; }

        public Task AddAsync(User entity, CancellationToken ct = default)
        => _db.Users.AddAsync(entity, ct).AsTask();

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
        }


        public Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<User>> SearchAsync(string q, int take = 50, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
