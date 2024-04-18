using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IService<T> // burada ki <T> interface in generic yani genel olarak kullanılacağını ifade ediyor.
    {
        #region Senkron Metotlar
        // Burada Service calssın da tüm claslarla ilgili ortak crud işlemlerini yapabilmemizi sağlayan metodların imzalarını belirliyoruz.
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);    // x=>x ... şeklinde sorgu yazabilmemiz i sağlayan mettod.
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();
        #endregion
        #region Asenkron Metotlar
        // Entity Framework deki asenkron metodları da reponsitory pattern de kullanmak istersek bu şekilde kullanmak isteriz. Aşğıda ki metot imzalarını servis classlarını da kullanmalıyız.
        Task<T> FindAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetIncludeAsync(Expression<Func<T, bool>> expression, string table);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task<int> SaveAsync();
        #endregion
    }
}
