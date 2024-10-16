
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Data
{
    public class Order_ItemConfig : IEntityTypeConfiguration<Order_Item>
    {
        public void Configure(EntityTypeBuilder<Order_Item> builder)
        {
            builder.ToTable("Order_Item").HasKey(x => x.OrderItem_ID);

            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).HasPrecision(10, 2).IsRequired();

            // معملتش علاقه الكتب و الاوردر عشان عملتها هناك 
        }
    }
}
