using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TranThanhDat_Exercises2.Data.Entities;

namespace TranThanhDat_Exercises2.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired().HasMaxLength(250);
        }
    }
}
