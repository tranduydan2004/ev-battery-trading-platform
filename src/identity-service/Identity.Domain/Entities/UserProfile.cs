using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Entities
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        [MaxLength(100)]
        public string UserFullName { get; set; }
        [MaxLength(200)]
        public string UserAddress { get; set; }
        public DateTime? UserBirthday { get; set; }

        // Lưu URL
        public string? Avatar { get ;set; }
        public string? CitizenIdCard { get;set; }
        [MaxLength(20)]
        public string? Status { get; set; }  // "verified" | "unverified"

        public decimal? TotalAmountPurchase { get; set; } = 0.000m;
        public decimal? TotalAmountSold { get; set; } = 0.000m;

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        private UserProfile() { }
        public UserProfile (int userId, string fullname, string address, DateTime? birthday, string? avatarUrl, string? citizenIdCard, string? status)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId phải lớn hơn 0.", nameof(userId));

            if (string.IsNullOrWhiteSpace(fullname))
                throw new ArgumentException("Fullname không được để trống.", nameof(fullname));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address không được để trống.", nameof(address));

            if (birthday.HasValue && birthday.Value > DateTime.UtcNow)
                throw new ArgumentException("Birthday không được ở tương lai.", nameof(birthday));

            if (!string.IsNullOrEmpty(status) &&
                status != "verified" && status != "unverified")
            {
                throw new ArgumentException("Status chỉ được là 'verified' hoặc 'unverified'.", nameof(status));
            }
            UserId = userId;
            UserFullName = fullname;
            UserAddress = address;
            UserBirthday = birthday;
            Avatar = avatarUrl;
            CitizenIdCard = citizenIdCard;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
