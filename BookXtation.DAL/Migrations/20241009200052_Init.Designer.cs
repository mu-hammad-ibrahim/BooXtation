﻿// <auto-generated />
using System;
using BookXtation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookXtation.DAL.Migrations
{
    [DbContext(typeof(BooXtationContext))]
    [Migration("20241009200052_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookXtation.DAL.Models.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Author", b =>
                {
                    b.Property<int>("Author_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Author_ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Author_ID");

                    b.ToTable("Author", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Book", b =>
                {
                    b.Property<int>("Book_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Book_ID"));

                    b.Property<decimal>("ActualPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Author_ID")
                        .HasColumnType("int");

                    b.Property<int>("Category_ID")
                        .HasColumnType("int");

                    b.Property<string>("Cover_Image")
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Language")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<int?>("Pages")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime?>("Publish_Date")
                        .HasColumnType("date");

                    b.Property<int>("Publisher_ID")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Book_ID");

                    b.HasIndex("Author_ID");

                    b.HasIndex("Category_ID");

                    b.HasIndex("ISBN")
                        .IsUnique();

                    b.HasIndex("Publisher_ID");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Cart_Item", b =>
                {
                    b.Property<int>("CartItem_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItem_ID"));

                    b.Property<int>("Book_ID")
                        .HasColumnType("int");

                    b.Property<int>("Cart_ID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartItem_ID");

                    b.HasIndex("Book_ID");

                    b.HasIndex("Cart_ID");

                    b.ToTable("Cart_Item", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Category", b =>
                {
                    b.Property<int>("Category_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Category_ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Category_ID");

                    b.HasIndex("Name");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Customer", b =>
                {
                    b.Property<int>("Customer_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Customer_ID"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("User_ID")
                        .HasColumnType("NVARCHAR(100)");

                    b.HasKey("Customer_ID");

                    b.HasIndex("User_ID")
                        .IsUnique()
                        .HasFilter("[User_ID] IS NOT NULL");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.CustomerDetails", b =>
                {
                    b.Property<int>("CustomerDetails_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerDetails_ID"));

                    b.Property<int>("Build")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR");

                    b.Property<int>("Customer_ID")
                        .HasColumnType("int");

                    b.Property<string>("DistinctiveMark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<int?>("Flat")
                        .HasColumnType("int");

                    b.Property<int?>("Floor")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("CustomerDetails_ID");

                    b.HasIndex("Customer_ID");

                    b.ToTable("CustomerDetails", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.FavouriteBooks", b =>
                {
                    b.Property<int>("Favourit_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Favourit_ID"));

                    b.Property<DateTime>("AddedDate")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(0)
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("Book_ID")
                        .HasColumnType("int");

                    b.Property<int>("Customer_ID")
                        .HasColumnType("int");

                    b.HasKey("Favourit_ID");

                    b.HasIndex("Book_ID");

                    b.HasIndex("Customer_ID");

                    b.ToTable("FavouriteBooks", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Order", b =>
                {
                    b.Property<int>("Order_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Order_ID"));

                    b.Property<int>("Customer_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(0)
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Order_ID");

                    b.HasIndex("Customer_ID");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetails_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetails_ID"));

                    b.Property<int>("Build")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("DistinctiveMark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<int?>("Flat")
                        .HasColumnType("int");

                    b.Property<int?>("Floor")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order_ID")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("OrderDetails_ID");

                    b.HasIndex("Order_ID")
                        .IsUnique();

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Order_Item", b =>
                {
                    b.Property<int>("OrderItem_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItem_ID"));

                    b.Property<int>("Book_ID")
                        .HasColumnType("int");

                    b.Property<int>("Order_ID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItem_ID");

                    b.HasIndex("Book_ID");

                    b.HasIndex("Order_ID");

                    b.ToTable("Order_Item", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Payment", b =>
                {
                    b.Property<int>("Payment_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Payment_ID"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("Order_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(0)
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Payment_ID");

                    b.HasIndex("Order_ID")
                        .IsUnique();

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Publisher", b =>
                {
                    b.Property<int>("Publisher_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Publisher_ID"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Contact")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Publisher_ID");

                    b.ToTable("Publisher", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Shopping_Cart", b =>
                {
                    b.Property<int>("Cart_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cart_ID"));

                    b.Property<DateTime>("Created_At")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(0)
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("Customer_ID")
                        .HasColumnType("int");

                    b.HasKey("Cart_ID");

                    b.HasIndex("Customer_ID");

                    b.ToTable("Shopping_Cart", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserUserLogin", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken", (string)null);
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Book", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("Author_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BookXtation.DAL.Models.Data.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("Category_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BookXtation.DAL.Models.Data.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("Publisher_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Category");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Cart_Item", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.Book", "book")
                        .WithMany("Cart_Items")
                        .HasForeignKey("Book_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BookXtation.DAL.Models.Data.Shopping_Cart", "Shopping_Cart")
                        .WithMany("Cart_Items")
                        .HasForeignKey("Cart_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Shopping_Cart");

                    b.Navigation("book");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Customer", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.ApplicationUser", "applicationUser")
                        .WithOne("Customer")
                        .HasForeignKey("BookXtation.DAL.Models.Data.Customer", "User_ID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("applicationUser");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.CustomerDetails", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.Customer", "Customer")
                        .WithMany("customerDetails")
                        .HasForeignKey("Customer_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.FavouriteBooks", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.Book", "Book")
                        .WithMany("FavouriteBooks")
                        .HasForeignKey("Book_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookXtation.DAL.Models.Data.Customer", "Customer")
                        .WithMany("FavouriteBooks")
                        .HasForeignKey("Customer_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Order", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("Customer_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.OrderDetails", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.Order", "Order")
                        .WithOne("OrderDetails")
                        .HasForeignKey("BookXtation.DAL.Models.Data.OrderDetails", "Order_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Order_Item", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.Book", "book")
                        .WithMany("Order_Items")
                        .HasForeignKey("Book_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BookXtation.DAL.Models.Data.Order", "Order")
                        .WithMany("Order_Items")
                        .HasForeignKey("Order_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("book");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Payment", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.Order", "Order")
                        .WithOne("Payment")
                        .HasForeignKey("BookXtation.DAL.Models.Data.Payment", "Order_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Shopping_Cart", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.Customer", "Customer")
                        .WithMany("Shopping_Carts")
                        .HasForeignKey("Customer_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookXtation.DAL.Models.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BookXtation.DAL.Models.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.ApplicationUser", b =>
                {
                    b.Navigation("Customer")
                        .IsRequired();
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Book", b =>
                {
                    b.Navigation("Cart_Items");

                    b.Navigation("FavouriteBooks");

                    b.Navigation("Order_Items");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Customer", b =>
                {
                    b.Navigation("FavouriteBooks");

                    b.Navigation("Orders");

                    b.Navigation("Shopping_Carts");

                    b.Navigation("customerDetails");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Order", b =>
                {
                    b.Navigation("OrderDetails")
                        .IsRequired();

                    b.Navigation("Order_Items");

                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookXtation.DAL.Models.Data.Shopping_Cart", b =>
                {
                    b.Navigation("Cart_Items");
                });
#pragma warning restore 612, 618
        }
    }
}
