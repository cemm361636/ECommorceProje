using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICategoryService
    {
        // Burada ICategoryService calssın da kategorilerle ilgili tüm crud işlemlerini yapabilmemizi sağlayan metodların imzalarını belirliyoruz.
        List<Category> GetCategories();
        Category GetCategory(int id);
        Category GetCategoryByProducts(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
        int Save();
    }
}
