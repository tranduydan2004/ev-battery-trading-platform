using Catalog.Domain.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) => _db = db;

        public Task AddAsync(Product entity, CancellationToken ct = default)
            => _db.Products.AddAsync(entity, ct).AsTask();

        public Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
            => _db.Products.Include(p => p.Details).FirstOrDefaultAsync(p => p.ProductId == id, ct);

        public Task<IReadOnlyList<Product>> SearchAsync(string q, int take = 50, CancellationToken ct = default)
            => _db.Products
                .Where(p => p.StatusProduct == ProductStatus.Available && EF.Functions.ILike(p.Title, $"%{q}%"))
                .OrderByDescending(p => p.CreatedAt)
                .Take(take)
                .ToListAsync(ct)
                .ContinueWith(t => (IReadOnlyList<Product>)t.Result, ct);
    }
}
