using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }
        // unif of work pattern : bir kayıt işleminden sonra işlemi hemen db ye yansıtmak yerine o kayıt işğlemiyele ilgili tüm işler tamamamlansın sonra tüm işler veri tabanına yansıtılsın.
        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Delete(int id)
        {
            var kayit = GetCategory(id);
            _context.Categories.Remove(kayit);

        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();

        }

        public Category? GetCategory(int id)
        {
            return _context.Categories.Find(id);
        }



        public Category GetCategoryByProducts(int id)
        {
            return _context.Categories.Include(x => x.Products).FirstOrDefault(x => x.Id == id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
