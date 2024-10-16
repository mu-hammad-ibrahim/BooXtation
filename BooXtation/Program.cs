using BookXtation.DAL.Models;
using BooXtation.BLL.Repositories.Interfaces;
using BooXtation.BLL.Repositories.Repository;
using BooXtation.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using BookXtation.DAL.Models.Data;
using System.Configuration;
using BookXtation.DAL;
using BooXtation.BLL.Repositories.Specifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Hadeer Config To Enable Auto Mapper 
builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));

// Hadeer To Get Options from Context
builder.Services.AddDbContext<BooXtationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Constr"));
}, ServiceLifetime.Scoped);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    //options.SignIn.RequireConfirmedAccount = true;
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<BooXtationContext>()
    .AddDefaultUI();
//.AddDefaultTokenProviders();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true; // Protect session cookie
});


// Hadeer DI Config Services

builder.Services.AddScoped(typeof(IBookRepository), typeof(BookRepository));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericReposiory<>));
builder.Services.AddScoped(typeof(IAuthorRepository), typeof(AuthorRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(IPublisherRepository), typeof(PublisherRepository));

// Cart_Item Services
builder.Services.AddScoped(typeof(ICart_ItemRepository), typeof(Cart_ItemRepository));
builder.Services.AddScoped(typeof(IShopping_CartRepository), typeof(Shopping_CartRepository));
builder.Services.AddScoped(typeof(IOrder_ItemRepository), typeof(Order_ItemRepository));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));
builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
builder.Services.AddScoped(typeof(ICustomerDetailsRepository), typeof(CustomerDetailsRepository));
builder.Services.AddScoped(typeof(IOrderDetailsRepository), typeof(OrderDetailsRepository));

builder.Services.AddScoped(typeof(IFavouriteBooksRepository), typeof(FavouriteBooksRepository));


//Customer Services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<EmailService>();
builder.Services.AddControllersWithViews();


var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    var dbContext = services.GetRequiredService<BooXtationContext>();
    await dbContext.Database.MigrateAsync();
    await BooXtationContextSeeding.SeedAsync(dbContext);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, ex.Message);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
