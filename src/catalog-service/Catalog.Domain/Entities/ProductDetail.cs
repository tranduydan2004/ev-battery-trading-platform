namespace Catalog.Domain.Entities
{
    public class ProductDetail
    {
        public int ProductDetailId { get; private set; }
        public int ProductId { get; private set; }
        public int ProductType { get; private set; }
        public string ProductName { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string RegistrationCard { get; private set; }
        public string FileUrl { get; private set; }
        public string ImageUrl { get; private set; }
        public string? Status { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }

        private ProductDetail() { }
        public ProductDetail(int productId, string productName, string description,
            int productType , string registrationCard, string fileUrl,
            string imageUrl, string? status = "Listed")
        {
            ProductId = productId;
            ProductName = productName;
            Description = description;
            ProductType = productType;
            RegistrationCard = registrationCard;
            FileUrl = fileUrl;
            ImageUrl = imageUrl;
            Status = status;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}

