using Entities;
using Microsoft.AspNetCore.Mvc;
using ECommorceProje.ExtensionMethods;
using Service;
using ECommorceProje.Models;

namespace ECommorceProje.Controllers
{
    public class CartController : Controller
    {
        private readonly IService<Product> _productService;
        private readonly IService<Order> _orderService;

        public CartController(IService<Product> productService, IService<Order> orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var model = new CartViewModel();
            model.Cart = GetCart();
            return View(model); // sepet sayfasına sepetteki ürünleri gönderiyoruz
        }
        [HttpPost]
        public IActionResult Index(CartViewModel cartViewModel)
        {
            cartViewModel.Cart = GetCart();
            if (ModelState.IsValid)
            {
                try
                {
                    _orderService.Add(cartViewModel.Order);
                    _orderService.Save();
                    TempData["ordermessage"] = "<div class='alert alert-success'>Siparişiniz Alındı!</div>";
                    HttpContext.Session.Remove("Cart");
                    return RedirectToAction("OrderResult");
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(cartViewModel);
        }
        public IActionResult OrderResult(CartViewModel cartViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _orderService.Add(cartViewModel.Order);
                    _orderService.Save();
                    TempData["ordermessage"] = "<div class='alert alert-success'>Siparişiniz Alındı!</div>";
                    HttpContext.Session.Remove("Cart");
                    return RedirectToAction("OrderResult");
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(cartViewModel);
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
