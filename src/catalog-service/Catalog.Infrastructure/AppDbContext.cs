using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;

namespace Catalog.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductDetail> ProductDetails => Set<ProductDetail>();

        protected override void OnModelCreating(ModelBuilder m)
        {
            base.OnModelCreating(m);

            // --- Cấu hình entity ---
            m.Entity<Product>(e => {
                e.HasKey(x => x.ProductId);
                e.Property(x => x.Title).HasMaxLength(100).IsRequired();
                e.Property(x => x.Price).HasColumnType("decimal(18,2)");
                e.Property(x => x.PickupAddress).HasMaxLength(300).IsRequired();
                e.Property(x => x.StatusProduct).HasConversion<int>();
                e.HasMany(x => x.Details).WithOne().HasForeignKey(d => d.ProductId);
                e.HasIndex(x => x.Title);
                e.HasQueryFilter(x => x.DeletedAt == null);
            });

            m.Entity<ProductDetail>(e => {
                e.HasKey(x => x.ProductDetailId);
                e.Property(x => x.ProductName).HasMaxLength(100).IsRequired();
                e.Property(x => x.Description).HasMaxLength(1000).IsRequired();
                e.Property(x => x.RegistrationCard).HasMaxLength(200);
                e.Property(x => x.FileUrl).HasMaxLength(200);
                e.Property(x => x.ImageUrl).HasMaxLength(200);
                e.HasIndex(x => x.ProductName);
                e.HasQueryFilter(x => x.DeletedAt == null);
            });

            // --- Chuyển toàn bộ sang snake_case ---
            foreach (IMutableEntityType entity in m.Model.GetEntityTypes())
            {
                // Tên bảng
                var tableName = entity.GetTableName();
                entity.SetTableName(ToSnakeCase(tableName!));

                // Cột
                foreach (var property in entity.GetProperties())
                {
                    var columnName = property.GetColumnName(StoreObjectIdentifier.Table(entity.GetTableName()!, null));
                    property.SetColumnName(ToSnakeCase(columnName!));
                }

                // Khóa
                foreach (var key in entity.GetKeys())
                {
                    key.SetName(ToSnakeCase(key.GetName()!));
                }

                // Index
                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()!));
                }
            }
        }

        // Hàm chuyển PascalCase → snake_case
        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Tách giữa chữ thường/số và chữ hoa, rồi lower toàn bộ
            var result = Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
            return result;
        }
    }
}
