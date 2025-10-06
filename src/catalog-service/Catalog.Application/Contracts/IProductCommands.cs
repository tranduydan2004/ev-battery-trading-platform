using Catalog.Application.DTOs;

namespace Catalog.Application.Contracts
{
    public interface IProductCommands
    {
        Task<int> CreateAsync(CreateProductDto dto, CancellationToken ct = default);
    }
}
