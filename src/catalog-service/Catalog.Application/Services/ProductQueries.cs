using Catalog.Application.Contracts;
using Catalog.Application.DTOs;
using Catalog.Domain.Abstractions;

namespace Catalog.Application.Services
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductRepository _repo;
        public ProductQueries(IProductRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<ProductBriefDto>> SearchAsync(string q, CancellationToken ct = default)
        {
            var items = await _repo.SearchAsync(q, 50, ct);
            return items.Select(p => new ProductBriefDto(p.ProductId, p.Title, p.Price)).ToList();
        }
    }
}
