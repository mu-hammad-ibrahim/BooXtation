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
    public class FavouriteBooksConfig : IEntityTypeConfiguration<FavouriteBooks>
    {
        public void Configure(EntityTypeBuilder<FavouriteBooks> builder)
        {
            builder.ToTable("FavouriteBooks").HasKey(x => x.Favourit_ID);

            builder.HasOne(x => x.Customer).WithMany(x => x.FavouriteBooks)
                 .HasForeignKey(x => x.Customer_ID);

            builder.HasOne(x => x.Book).WithMany(x => x.FavouriteBooks)
                 .HasForeignKey(x => x.Book_ID);

            builder.Property(x => x.AddedDate).HasColumnType("datetime2")
              .HasPrecision(0).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
