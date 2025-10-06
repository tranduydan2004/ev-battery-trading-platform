using Catalog.Application.DTOs;
using Catalog.Application.Contracts;
using Catalog.Domain.Abstractions;
using Catalog.Domain.Entities;

namespace Catalog.Application.Services
{
    public class ProductCommands : IProductCommands
    {
        private readonly IProductRepository _repo;
        private readonly IUnitOfWork _uow;
        public ProductCommands(IProductRepository repo, IUnitOfWork uow) { _repo = repo; _uow = uow; }

        public async Task<int> CreateAsync(CreateProductDto dto, CancellationToken ct = default)
        {

            var product = Product.Create(dto.Title, dto.Price, dto.SellerId, dto.PickupAddress, 1);
            product.Approve(adminId: 1);
            await _repo.AddAsync(product, ct);
            await _uow.SaveChangesAsync(ct);
            product.AddDetail(dto.ProductName, dto.Description, productType: dto.ProductType, registrationCard: dto.RegistrationCard, fileUrl: dto.FileUrl, imageUrl: dto.ImageUrl);
            await _uow.SaveChangesAsync(ct);
            return product.ProductId;
        }
    }
}
