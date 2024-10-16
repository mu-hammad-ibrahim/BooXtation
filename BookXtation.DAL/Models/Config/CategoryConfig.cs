using BookXtation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Data
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category").HasKey(x => x.Category_ID);

            builder.Property(x => x.Name).HasColumnType("NVARCHAR")
                .HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Name);

            builder.HasMany(x => x.Books).WithOne(x => x.Category)
                .HasForeignKey(x => x.Category_ID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
