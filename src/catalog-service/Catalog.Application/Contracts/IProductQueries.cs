using Catalog.Application.DTOs;

namespace Catalog.Application.Contracts
{
    public interface IProductQueries
    {
        Task<IReadOnlyList<ProductBriefDto>> SearchAsync(string q, CancellationToken ct = default);
    }
}

