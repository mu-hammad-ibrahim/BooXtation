using BookXtation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Data
{
    public class Shopping_cartConfig : IEntityTypeConfiguration<Shopping_Cart>
    {
        public void Configure(EntityTypeBuilder<Shopping_Cart> builder)
        {
            builder.ToTable("Shopping_Cart").HasKey(x => x.Cart_ID);
            builder.Property(x => x.Created_At)
                .HasColumnType("datetime2")  // Use datetime2 to include full date and time precision
                .HasPrecision(0)             // Includes year, month, day, hour, minute, second (0 decimal places)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()"); ///// =====> بص عليها عملتها كده عشان ينزل تاريخ اللحظه اللي بعت فيها اوتوماتيك 

            builder.HasMany(x => x.Cart_Items).WithOne(x => x.Shopping_Cart)
                .HasForeignKey(x => x.Cart_ID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
