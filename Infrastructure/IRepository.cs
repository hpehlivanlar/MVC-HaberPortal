using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HaberSistemi.Core.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Verilerin tamamını ve tek tek çekme işlemi yapılıyor 
        IEnumerable<T> GetAll();
        
        T GetById(int id);

        T Get(Expression<Func<T, bool>> expression);

        IQueryable<T> GetMany(Expression<Func<T, bool>> expression);

        int Count();

        void Save();

        void Insert(T obj);

        void Update(T obj);

        void Delete(int id);

     
    }
}
