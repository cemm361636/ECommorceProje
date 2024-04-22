using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace ECommorceProje.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class MainController : Controller
    {
        private readonly DatabaseContext _context; // sağ tık > ampul > generate constructor

        public MainController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var kullanici = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.IsActive);
                if (kullanici == null)
                {
                    TempData["Message"] = "<p class='alert alert-danger mt-3'>Giriş Başarısız!</p>";
                }
                else
                {
                    var haklar = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, kullanici.Email),
                        new Claim(ClaimTypes.Name, kullanici.Name),
                        new Claim(ClaimTypes.Role, kullanici.IsAdmin ? "Admin" : "Personal")
                    };
                    var kullaniciKimligi = new ClaimsIdentity(haklar, "Login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(kullaniciKimligi);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    string donusAdresi = HttpContext?.Request?.Query?["ReturnUrl"];
                    if (!string.IsNullOrEmpty(donusAdresi))
                    {
                        return Redirect(donusAdresi);
                    }
                    return Redirect("/Admin");
                }
            }
            catch (Exception hata)
            {
                TempData["Message"] = hata?.InnerException?.Message;
            }
            return View();
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}

