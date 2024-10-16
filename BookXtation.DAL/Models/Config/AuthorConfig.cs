using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Config
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author").HasKey(x => x.Author_ID);

            builder.Property(x => x.Name).HasColumnType("NVARCHAR").HasMaxLength(150).IsRequired();

            builder.HasMany(x => x.Books).WithOne(x => x.Author)
                .HasForeignKey(x => x.Author_ID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
