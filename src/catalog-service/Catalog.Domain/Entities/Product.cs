using Catalog.Domain.Enums;

namespace Catalog.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; private set; }
        public string Title { get; private set; } = default!;
        public decimal Price { get; private set; }
        public int SellerId { get; private set; }
        public ProductStatus StatusProduct { get; private set; }
        public int Quantity { get; private set; }
        public int Sold { get; private set; }
        public DateTimeOffset? ModerationAt { get; private set; }
        public int? ModeratedBy { get; private set; }
        public string PickupAddress { get; private set; } = default!;
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }

        private readonly List<ProductDetail> _details = new();
        public IReadOnlyList<ProductDetail> Details => _details;

        private Product() { }

        public static Product Create(string title, decimal price, int sellerId, string pickupAddress, int quantity = 1)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required");
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            return new Product
            {
                Title = title,
                Price = price,
                SellerId = sellerId,
                PickupAddress = pickupAddress,
                Quantity = quantity,
                StatusProduct = ProductStatus.Pending,
                CreatedAt = DateTimeOffset.UtcNow
            };
        }

        public void Approve(int adminId)
        {
            StatusProduct = ProductStatus.Available;
            ModeratedBy = adminId;
            ModerationAt = DateTimeOffset.UtcNow;
            UpdatedAt = ModerationAt;
        }

        public ProductDetail AddDetail(string productName, string description, int productType ,
            string registrationCard, string fileUrl, string imageUrl, string? status = "Listed")
        {
            var d = new ProductDetail(ProductId, productName, description, productType, registrationCard, fileUrl, imageUrl, status);
            _details.Add(d);
            return d;
        }
    }
}
