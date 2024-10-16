
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookXtation.DAL.Models.Data
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer").HasKey(x => x.Customer_ID);

            builder.Property(x => x.FirstName).HasColumnType("NVARCHAR")
                .HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasColumnType("NVARCHAR")
                .HasMaxLength(50).IsRequired();
            builder.Property(x => x.Phone).HasColumnType("NVARCHAR")
                .HasMaxLength(20).IsRequired();

            builder.Property(x => x.DateOfBirth).HasColumnType("date")
                .IsRequired(false);

            builder.HasMany(x => x.Orders).WithOne(x => x.Customer)
                .HasForeignKey(x => x.Customer_ID).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Shopping_Carts).WithOne(x => x.Customer)
                .HasForeignKey(x => x.Customer_ID).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.applicationUser).WithOne(x =>x.Customer)
                .HasForeignKey<Customer>(x => x.User_ID).OnDelete(DeleteBehavior.SetNull);

            //builder.HasMany(x => x.Reviews).WithOne(x => x.Customer).
            //    HasForeignKey(x => x.Customer_ID).OnDelete(deleteBehavior: DeleteBehavior.Restrict);


        }
    }
}
