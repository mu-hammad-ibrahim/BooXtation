using BookXtation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Data
{
    public class PublisherConfig : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publisher").HasKey(x => x.Publisher_ID);

            builder.Property(x => x.Name).HasColumnType("NVARCHAR")
                .HasMaxLength(150).IsRequired();
            builder.Property(x => x.Contact).HasColumnType("NVARCHAR")
                .HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.Address).HasColumnType("NVARCHAR")
                .HasMaxLength(255).IsRequired(false);

            builder.HasMany(x => x.Books).WithOne(x => x.Publisher)
                .HasForeignKey(x => x.Publisher_ID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
