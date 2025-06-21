using Hockeyshop.Data.Data;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.Interfaces.CMS;
using Hockeyshop.Interfaces.Orders;
using Hockeyshop.Interfaces.Products;
using Hockeyshop.Services.CMS;
using Hockeyshop.Services.CMS;
using Hockeyshop.Services.Orders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IWelcomeTextService, WelcomeTextService>();
builder.Services.AddScoped<IFooterSectionService, FooterSectionService>();
builder.Services.AddScoped<IContactSectionService, ContactSectionService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IContactMessageService, ContactMessageService>();


builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient(); //SignalR

//obsługa sesji
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
    });

builder.Services.AddDbContext<HockeyshopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HockeyshopContext")
        ?? throw new InvalidOperationException("Connection string 'HockeyshopContext' not found.")));

var app = builder.Build();

//konfig dla http
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();//middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();