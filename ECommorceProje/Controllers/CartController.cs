using Entities;
using Microsoft.AspNetCore.Mvc;
using ECommorceProje.ExtensionMethods;
using Service;

namespace ECommorceProje.Controllers
{
    public class CartController : Controller
    {
        private readonly IService<Product> _productService;

        public CartController(IService<Product> productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(GetCart()); // sepet sayfasına sepetteki ürünleri gönderiyoruz
        }
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = _productService.Get(productId);
            if (product != null)
            {
                var cart = GetCart(); // sepeti getir metodu

                cart.AddProduct(product, quantity); // sepete ekliyoruz
                SaveCart(cart); // sepeti kaydediyoruz
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int productId)
        {
            var product = _productService.Get(productId);
            if (product != null)
            {
                var cart = GetCart(); // sepeti getir metodu
                cart.RemoveProduct(product); // ürünü sepetten çıkar
                SaveCart(cart); // sepeti güncelle
            }
            return RedirectToAction("Index");
        }
        // sepete ekle metodu
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart); // gelen cart nesnesini kendi yazdığımız sessionda json saklama metoduna yolluyoruz
        }
        // sepeti getir metodu
        private Cart GetCart()
        {
            return HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart(); // kendi yazdığımız sessionda json saklama metodundaki sepeti çekip metodun çağrıldığı yere yolluyoruz
        }
    }
}
