
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Data
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book").HasKey(x => x.Book_ID);

            builder.Property(x => x.Title).HasColumnType("NVARCHAR")
                .HasMaxLength(200).IsRequired();

            builder.Property(x => x.ISBN).HasColumnType("NVARCHAR")
                .HasMaxLength(13).IsRequired();
            builder.HasIndex(x => x.ISBN).IsUnique();

            builder.Property(x => x.Summary).HasColumnType("NVARCHAR")
                .HasMaxLength(500).IsRequired(false);

            builder.Property(x => x.Price).HasPrecision(10, 2).IsRequired();

            builder.Property(x => x.Stock).IsRequired();

            builder.Property(x => x.Pages).IsRequired(false);

            builder.Property(x => x.Language).HasColumnType("NVARCHAR")
                .HasMaxLength(50).IsRequired(false);

            builder.Property(x => x.Publish_Date).HasColumnType("date")
                .IsRequired(false);

            builder.Property(x => x.Cover_Image).HasColumnType("NVARCHAR")
                .HasMaxLength(255).IsRequired(false);


            builder.HasMany(x => x.Cart_Items).WithOne(x => x.book)
                .HasForeignKey(x => x.Book_ID).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Order_Items).WithOne(x => x.book)
                .HasForeignKey(x => x.Book_ID).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category)
               .WithMany(x => x.Books)
               .HasForeignKey(x => x.Category_ID)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Publisher)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.Publisher_ID)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(x => x.Reviews).WithOne(x => x.Book)
            //    .HasForeignKey(x => x.Book_ID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
