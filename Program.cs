using Microsoft.EntityFrameworkCore;
using PortfolioAdminPanel.Data;
using Microsoft.AspNetCore.Authentication.Cookies;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Eğer giriş yapmamış biri panele girmeye çalışırsa onu bu adrese yönlendir
        options.LoginPath = "/Auth/Login";
        // Kullanıcının tarayıcısına bırakılacak güvenli çerezin adı
        options.Cookie.Name = "PortfolioAdminAuth";
    });

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
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    // "Project" yazan yeri "Home" olarak değiştirdik
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulama başlarken veritabanı yoksa veya eksik tablolar varsa otomatik oluşturur (Docker için kritik)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.Run();
