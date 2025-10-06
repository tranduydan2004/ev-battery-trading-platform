using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;

namespace Identity.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

        protected override void OnModelCreating(ModelBuilder m)
        {
            base.OnModelCreating(m);

            // --- Cấu hình entity thủ công (vẫn giữ) ---
            m.Entity<User>(e =>
            {
                e.HasKey(x => x.UserId);
                e.Property(x => x.UserEmail).HasMaxLength(50).IsRequired();
                e.Property(x => x.UserPhone).HasMaxLength(20).IsRequired();
                e.Property(x => x.UserPassword).HasMaxLength(256).IsRequired();
                e.HasMany(x => x.UserProfiles).WithOne().HasForeignKey(e => e.UserId);
                e.HasQueryFilter(x => x.DeletedAt == null);
            });

            m.Entity<UserProfile>(e =>
            {
                e.HasQueryFilter(x => x.DeletedAt == null);
            });

            // --- Chuyển toàn bộ sang snake_case ---
            foreach (IMutableEntityType entity in m.Model.GetEntityTypes())
            {
                // Tên bảng
                entity.SetTableName(ToSnakeCase(entity.GetTableName()!));

                // Cột
                foreach (var property in entity.GetProperties())
                    property.SetColumnName(ToSnakeCase(property.GetColumnName(StoreObjectIdentifier.Table(entity.GetTableName()!, null))!));

                // Khóa
                foreach (var key in entity.GetKeys())
                    key.SetName(ToSnakeCase(key.GetName()!));

                // Chỉ mục
                foreach (var index in entity.GetIndexes())
                    index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()!));
            }
        }

        private static string ToSnakeCase(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            return Regex.Replace(name, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}
