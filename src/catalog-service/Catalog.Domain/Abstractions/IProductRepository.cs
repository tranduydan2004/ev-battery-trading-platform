using Catalog.Domain.Entities;

namespace Catalog.Domain.Abstractions
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Product>> SearchAsync(string q, int take = 50, CancellationToken ct = default);
        Task AddAsync(Product entity, CancellationToken ct = default);
    }
}
