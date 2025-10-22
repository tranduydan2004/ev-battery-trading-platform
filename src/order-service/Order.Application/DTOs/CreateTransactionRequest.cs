namespace Order.Application.DTOs
{
    public class CreateTransactionRequest
    {
        public int ProductId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int FeeId { get; set; }
        public int ProductType { get; set; }
        public decimal BasePrice { get; set; }
        public decimal CommissionFee { get; set; }
        public decimal? ServiceFee { get; set; }
        public decimal BuyerAmount { get; set; }
        public decimal SellerAmount { get; set; }
        public decimal PlatformAmount { get; set; }
    }
}