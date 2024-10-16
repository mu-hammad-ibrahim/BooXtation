
using BookXtation.DAL.Models.Config;
using BookXtation.DAL.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace BookXtation.DAL.Models
{
    public class BooXtationContext : IdentityDbContext<ApplicationUser>
    {

        public BooXtationContext(DbContextOptions<BooXtationContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors {  get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<FavouriteBooks> FavouriteBooks { get; set; }
        public DbSet<Cart_Item> Cart_Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        //public DbSet<Review> Reviews { get; set; }
        public DbSet<Shopping_Cart> Shopping_Carts { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            var connection = configuration.GetSection("ConnectionString").Value;

            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfig).Assembly);

            //Hadeer This Line To Apply Configration For ever class Inhert From IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserUserLogin");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Book>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.CalculatePrice();
                }
            }
            return base.SaveChanges();
        }
    }
}
