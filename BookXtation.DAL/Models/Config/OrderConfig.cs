using BookXtation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Data
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order").HasKey(x => x.Order_ID);

            builder.Property(x => x.OrderDate)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()"); 

            builder.Property(x => x.TotalAmount).HasPrecision(10, 2).IsRequired();
            builder.Property(x => x.OrderStatus).HasColumnType("NVARCHAR")
                .HasMaxLength(50).IsRequired();

            
            builder.Property(x => x.PaymentStatus).HasColumnType("NVARCHAR")
                .HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.Order_Items).WithOne(x => x.Order)
                .HasForeignKey(x => x.Order_ID).OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Payment).WithOne(x => x.Order)
                .HasForeignKey<Payment>(x => x.Order_ID).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
