using BookXtation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Data
{
    public class UserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("User").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("NVARCHAR")
                .HasMaxLength(100).IsRequired();

            builder.Property(x => x.CreatedAt).HasColumnType("date")
                .IsRequired().HasDefaultValueSql("GETDATE()");

            

        }
    }
}
