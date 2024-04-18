namespace Entities
{
    public class Cart
    {
        private List<CartLine> products = new(); // Bu class içerisindeki işlemlerde kullanacağımız private liste
        public List<CartLine> Products => products; // dışarıda kullanacağımız public ürün listesi
        public void AddProduct(Product product, int quantity) // sepete ekleme için beklediğimiz 2 parametre
        {
            var prd = products.Where(i => i.Product.Id == product.Id) // products listesine bak, gelen ürün id si ile eşleşen kayıt var mı
                .FirstOrDefault(); // sonuca göre ilk kaydı al
            if (prd == null) // eğer sepette ürün yoksa 
            {
                products.Add(new CartLine() // ürünü sepete ekle
                {
                    Product = product, // sepete eklenecek ürün
                    Quantity = quantity // müşterinin yolladığı miktar
                });
            }
            else // eğer sepette ürün varsa
            {
                prd.Quantity += quantity; // sepetteki ürünün miktarını müşterinin gönderdiği değer kadar artır.
            }
        }
        public void RemoveProduct(Product product) // dışarıdan silinmek istenen ürünü al
        {
            products.RemoveAll(i => i.Product.Id == product.Id); // ürün listesinden id si eşleşeni sil
        }
        public decimal TotalPrice()
        {
            return products.Sum(i => i.Product.Price * i.Quantity); // sepetteki ürünlerin toplam tutarını dönecek olan metot. Ürün fiyatıyla miktarı çarptırıp döndük.
        }
        public void ClearAll() // Sepeti Boşalt Metodu
        {
            products.Clear(); // sepetteki tüm ürünleri temizle
        }
    }
}