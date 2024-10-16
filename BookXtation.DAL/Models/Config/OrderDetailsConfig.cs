using BookXtation.DAL.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.Config
{
    public class OrderDetailsConfig : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails").HasKey(x => x.OrderDetails_ID);

            builder.Property(x => x.FirstName).HasColumnType("NVARCHAR")
               .HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasColumnType("NVARCHAR")
                .HasMaxLength(50).IsRequired();
            builder.Property(x => x.City).HasColumnType("NVARCHAR").HasMaxLength(20).IsRequired();
            builder.Property(x => x.street).HasColumnType("NVARCHAR").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Build).IsRequired();
            builder.Property(x => x.Phone).HasColumnType("NVARCHAR").HasMaxLength(11).IsRequired();

            builder.Property(x => x.Floor).IsRequired(false);
            builder.Property(x => x.Flat).IsRequired(false);
            builder.Property(x => x.DistinctiveMark).IsRequired(false);
            builder.Property(x => x.Location).IsRequired(false);


            builder.HasOne(x => x.Order).WithOne(x => x.OrderDetails)
                .HasForeignKey<OrderDetails>(x => x.Order_ID);
        }
    }
}
