using Order.Domain.Enums;
namespace Order.Domain.Entities
{
    public class Transaction
    {
        // Thuộc tính chính
        public int TransactionId { get; private set; }
        public int ProductId { get; private set; } // ID của Xe/Pin
        public int SellerId { get; private set; } // ID của người bán
        public int BuyerId { get; private set; } // ID của người mua
        public int FeeId { get; private set; } // ID của phí dịch vụ
        public int ProductType { get; private set; } // Loại sản phẩm: Xe/Pin
        public Enums.TransactionStatus TransactionStatus { get; private set; } // Trạng thái giao dịch
        public decimal BasePrice { get; private set; } // Giá gốc
        public decimal CommissionFee { get; private set; } // Phí hoa hồng
        public decimal? ServiceFee { get; private set; } // Phí dịch vụ (nếu có)
        public decimal BuyerAmount { get; private set; } // Số tiền người mua phải trả
        public decimal SellerAmount { get; private set; } // Số tiền người bán nhận được
        public decimal PlatformAmount { get; private set; } // Số tiền nền tảng nhận được
        public DateTimeOffset CreatedAt { get; private set; } // Thời gian tạo giao dịch
        public DateTimeOffset? UpdatedAt { get; private set; } // Thời gian cập nhật giao dịch gần nhất (nếu có)
        public DateTimeOffset? DeletedAt { get; private set; } // Thời gian xóa giao dịch (nếu có)

        // Constructor rỗng cho EF Core
        protected Transaction() { }

        // Constructor chính để tạo đơn hàng mới (nghiệp vụ)
        public Transaction(int productId, int sellerId, int buyerId, int feeId, int productType, decimal basePrice, decimal commissionFee, decimal? serviceFee, decimal buyerAmount, decimal sellerAmount, decimal platformAmount)
        {
            // Bổ sung Validation (ArgumentException)
            if (productId <= 0)
                throw new ArgumentException("Product ID must be valid.", nameof(productId));

            if (sellerId <= 0)
                throw new ArgumentException("Seller ID must be valid.", nameof(sellerId));

            if (buyerId <= 0)
                throw new ArgumentException("Buyer ID must be valid.", nameof(buyerId));

            if (basePrice <= 0)
                throw new ArgumentException("Base price must be positive.", nameof(basePrice));

            if (buyerAmount <= 0)
                throw new ArgumentException("Buyer amount must be positive.", nameof(buyerAmount));

            if (platformAmount < 0)
                throw new ArgumentException("Platform amount cannot be negative.", nameof(platformAmount));

            ProductId = productId;
            SellerId = sellerId;
            BuyerId = buyerId;
            FeeId = feeId;
            ProductType = productType;
            TransactionStatus = Enums.TransactionStatus.Pending; // Mặc định trạng thái là Pending khi tạo mới
            BasePrice = basePrice;
            CommissionFee = commissionFee;
            ServiceFee = serviceFee;
            BuyerAmount = buyerAmount;
            SellerAmount = sellerAmount;
            PlatformAmount = platformAmount;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        // Phương thức nghiệp vụ để cập nhật trạng thái đơn hàng
        public void UpdateStatus(Enums.TransactionStatus newStatus)
        {
            TransactionStatus = newStatus;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        // Có thể thêm các phương thức khác như Cancel, SoftDelete, ... nếu cần
        public void MarkAsDeleted()
        {
            DeletedAt = DateTimeOffset.UtcNow;
        }
    }
}
