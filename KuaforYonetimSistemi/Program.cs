using KuaforYonetimSistemi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// 📌 Veritabanı Bağlantısı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 📌 ASP.NET Core Identity Yapılandırması
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 📌 Yetkilendirme Politikaları
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
});

// 📌 MVC Servisleri
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 📌 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// 📌 Rota Yapılandırması
app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=AdminDashboard}/{id?}",
    defaults: new { controller = "Admin" });

app.MapControllerRoute(
    name: "customer",
    pattern: "Customer/{action=CustomerDashboard}/{id?}",
    defaults: new { controller = "Customer" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
