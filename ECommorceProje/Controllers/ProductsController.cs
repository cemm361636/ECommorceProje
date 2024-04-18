
using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace ECommorceProje.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICategoryService _categoryService; // 1. de sağ klik > ampul > generate constructor diyoruz.
        private readonly DatabaseContext _dbContext; // sağ klik ampul > add parameters to menüsüyle birden fazla injection ı ekliyoruz!! 
        private readonly IService<Product> _service; // Generic olan Iservice metodumuz 1 class nesnesiyle çalışır. Bu şekilde tüm entity class larımızı göndererek aynı servis ile db işlemlerini repository pattern sayesinde gerçekleştirebiliyoruz.
        public ProductsController(ICategoryService categoryService, DatabaseContext dbContext, IService<Product> service)
        {
            _categoryService = categoryService;
            _dbContext = dbContext;
            _service = service;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var databaseContext = _dbContext.Products.Include(p => p.Category);
            return View(await databaseContext.ToListAsync());
        }
        public async Task<IActionResult> DetailAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var model = await _dbContext.Products.Where(x => x.Id == id && x.IsActive).Include(x => x.Category).FirstOrDefaultAsync();
            // var model = _service.Get(id.Value);
            if (model is null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
