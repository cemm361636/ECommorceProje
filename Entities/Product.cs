using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product : IEntity
    {

        public int Id { get; set; }
        [Display(Name = "Adı"), StringLength(150), Required(ErrorMessage = "{0} Boş Geçilemez")]
        public string Name { get; set; }
        [Display(Name = "Ürün Açıklaması"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Resim"), StringLength(100)]
        public string? Image { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Anasayfada Göster")]
        public bool IsHome { get; set; }
        [Display(Name = "Stok"), Required(ErrorMessage = "{0} Boş Geçilemez")]
        public int Stock { get; set; }
        [Display(Name = "Fiyat"), Required(ErrorMessage = "{0} Boş Geçilemez")]
        public decimal Price { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public virtual Category? Category { get; set; }
    }
}
