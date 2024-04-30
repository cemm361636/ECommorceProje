
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
builder.Services.AddDbContext<DatabaseContext>(); // Veritaban� i�lemleri i�in

// �zel hizmetinizi kaydedin
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IUserService, UserService>();



// Genel servis i�in hizmet sa�lay�c�s� kayd�n� kald�r�n
// builder.Services.AddScoped(typeof(IService<>), typeof(IService<>)); 

var app = builder.Build();

// HTTP iste�i boru hatt�n� yap�land�r�n
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession(); //Session kullanabilmek i�in yukar�da servis larak ekledikten sonra burda kullanmak istedi�imizi belirtiyoruz.
app.UseRouting();
app.UseAuthentication(); // Kimlik do�rulamay� kullanma
app.UseAuthorization();

// Projeye alan ekleme
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
);

// Varsay�lan y�nlendirme
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "custom",
    pattern: "{customurl?}/{controller=Home}/{action=Index}/{id?}");

app.Run();