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
}
