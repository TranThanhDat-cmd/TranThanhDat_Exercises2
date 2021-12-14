using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranThanhDat_Exercises2.Data.Entities;
namespace TranThanhDat_Exercises2.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired().HasMaxLength(300);
            builder.Property(x => x.Price).IsRequired();
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products).HasForeignKey(x => x.CategoryID)
                .HasConstraintName("FK_Product_Category");
        }
    }
}
