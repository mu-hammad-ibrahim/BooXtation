using BookXtation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Data
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment").HasKey(x => x.Payment_ID);

            builder.Property(x => x.PaymentDate).HasColumnType("datetime2")
                .HasPrecision(0).IsRequired().HasDefaultValueSql("GETDATE()");
           

            builder.Property(x => x.PaymentMethod).HasColumnType("NVARCHAR")
                .HasMaxLength(50).IsRequired();

            builder.Property(x => x.Amount).HasPrecision(10, 2).IsRequired();

            builder.Property(x => x.PaymentStatus).HasColumnType("NVARCHAR")
                .HasMaxLength(50).IsRequired();

        

        }
    }
}
