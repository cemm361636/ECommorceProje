using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
namespace ECommorceProje.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    public class MainController : Controller
    {
        [HttpGet]
        public IActionResult Logout()
        {
            // Oturumu sonlandır
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Başarılı bir şekilde çıkış yapıldığını belirten bir bildirim göster
            TempData["SuccessMessage"] = "Başarıyla çıkış yaptınız.";

            // Mevcut sayfada kalmak istiyorsanız, RedirectToAction kullanmayın
            // Ancak, isterseniz kullanıcıyı başka bir sayfaya yönlendirebilirsiniz
            return RedirectToAction("Login", "Account");
        }
    }



}
