using Hockeyshop.Data.Data;
using Hockeyshop.Interfaces;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.Interfaces.Products;
using Hockeyshop.Services;
using Hockeyshop.Services.CMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HockeyshopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HockeyshopContext") ?? throw new InvalidOperationException("Connection string 'HockeyshopContext' not found.")));

//serwisy
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IIconLibraryService, IconLibraryService>();
builder.Services.AddScoped<IShopRuleService, ShopRuleService>();
builder.Services.AddScoped<IWelcomeTextService, WelcomeTextService>();
builder.Services.AddScoped<IFooterSectionService, FooterSectionService>();
builder.Services.AddScoped<IContactSectionService, ContactSectionService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
