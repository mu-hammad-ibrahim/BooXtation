using BookXtation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Data
{
    public class Cart_ItemConfig : IEntityTypeConfiguration<Cart_Item>
    {
        public void Configure(EntityTypeBuilder<Cart_Item> builder)
        {
            builder.ToTable("Cart_Item").HasKey(x => x.CartItem_ID);

            builder.Property(x => x.Quantity);
            builder.Property(x => x.Price).HasPrecision(10, 2);


            ////// معملتش علاقه واحد للكتير عشان عملتها في الكتب و الشوبينج كارت و بص عليها بردو
        }
    }
}
