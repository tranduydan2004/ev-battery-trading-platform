using Identity.Domain.Enums;

namespace Identity.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public UserStatus? UserStatus { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        private readonly List<UserProfile> _userProfiles = new List<UserProfile>();
        public IReadOnlyList<UserProfile> UserProfiles => _userProfiles;

        private User() { }
        public static User Create(string email, string phone, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required", nameof(email));

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone is required", nameof(phone));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required", nameof(password));

            return new User
            {
                UserEmail = email,
                UserPhone = phone,
                UserPassword = password,
                UserStatus = Enums.UserStatus.Active,
                CreatedAt = DateTimeOffset.UtcNow
            };
        }

        public UserProfile AddProfile(string fullname, string address, DateTime? birthday, string? avatarUrl, string? citizenIdCard)
        {
            var p = new UserProfile(UserId, fullname, address, birthday, avatarUrl, citizenIdCard, "unverified");
            _userProfiles.Add(p);
            return p;
        }

    }
}
