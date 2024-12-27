using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebProgramlama.Data;

var builder = WebApplication.CreateBuilder(args);

// 📊 Veritabanı Bağlantısı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔑 Kimlik Doğrulama ve Yetkilendirme
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// 🖥️ MVC ve Razor View Servisleri
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🎭 Roller ve İlk Kullanıcıların Oluşturulması
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Admin Rolü Kontrolü
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Customer Rolü Kontrolü
    if (!await roleManager.RoleExistsAsync("Customer"))
    {
        await roleManager.CreateAsync(new IdentityRole("Customer"));
    }

    // Admin Kullanıcısı Oluşturma
    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    if (adminUser == null)
    {
        var newAdmin = new IdentityUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newAdmin, "Admin@1234");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}

// 🌐 Middleware Ayarları
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 🛠️ Rota Tanımları
app.MapControllerRoute(
    name: "appointment",
    pattern: "Appointment/{action=Index}/{id?}",
    defaults: new { controller = "Appointment" });

app.MapControllerRoute(
    name: "employee",
    pattern: "Employee/{action=Index}/{id?}",
    defaults: new { controller = "Employee" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// 🛡️ Hata Ayıklama Middleware (Opsiyonel)
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        throw;
    }
});

// 🔄 Uygulama Başlat
app.Run();
