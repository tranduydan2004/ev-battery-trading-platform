using Identity.Domain.Abtractions;

namespace Identity.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db) => _db = db;
        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
    }
}
