namespace Catalog.Application.DTOs
{
    public class CreateProductDto
    {
        public string Title { get; set; } = default!;
        public decimal Price { get; set; }
        public int ProductType { get; set; }
        public int SellerId { get; set; }
        public string PickupAddress { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string RegistrationCard { get; set; }
        public string FileUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}