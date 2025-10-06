using Identity.Domain.Enums;

namespace Identity.Application.DTOs
{
    public class UserResponseDto
    {
        public string UserEmail { get; private set; }
        public string UserPhone { get; private set; }
        public string UserPassword { get; private set; }
        public UserStatus UserStatus { get; private set; }
        public string UserFullName { get; private set; }
        public string UserAddress { get; private set; }
        public DateTime? UserBirthday { get; private set; }

        // Lưu URL
        public string? Avatar { get; private set; }
        public string? CitizenIdCard { get; private set; }
        public string? Status { get; private set; }  // "verified" | "unverified"
    }
}
