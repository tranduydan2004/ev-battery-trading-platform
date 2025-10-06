using Identity.Domain.Enums;

namespace Identity.Application.DTOs
{
    public class CreateUserDto
    {
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public string UserFullName { get; set; }
        public string UserAddress { get; set; }
        public DateTime? UserBirthday { get; set; }

        // Lưu URL
        public IFormFile? Avatar { get; set; }
        public IFormFile? CitizenIdCard { get; set; }
        public string? Status { get; set; }  // "verified" | "unverified"
    }
}
