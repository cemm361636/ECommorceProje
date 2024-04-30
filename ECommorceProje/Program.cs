
using Data;
using Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Service;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Hizmetlere kontrolleri ekleyin.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Main/Login";
    x.AccessDeniedPath = "/AccessDenied";
    x.Cookie.Name = "Account";
    x.Cookie.MaxAge = TimeSpan.FromDays(1);
    x.Cookie.IsEssential = true;
});
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "SuperAdmin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "SuperAdmin", "User", "Personal"));
});
builder.Services.AddDbContext<DatabaseContext>(); // Veritabaný iþlemleri için

// Özel hizmetinizi kaydedin
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IUserService, UserService>();



// Genel servis için hizmet saðlayýcýsý kaydýný kaldýrýn
// builder.Services.AddScoped(typeof(IService<>), typeof(IService<>)); 

var app = builder.Build();

// HTTP isteði boru hattýný yapýlandýrýn
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession(); //Session kullanabilmek için yukarýda servis larak ekledikten sonra burda kullanmak istediðimizi belirtiyoruz.
app.UseRouting();
app.UseAuthentication(); // Kimlik doðrulamayý kullanma
app.UseAuthorization();

// Projeye alan ekleme
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
);

// Varsayýlan yönlendirme
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "custom",
    pattern: "{customurl?}/{controller=Home}/{action=Index}/{id?}");

app.Run();