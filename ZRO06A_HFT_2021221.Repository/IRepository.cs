using System.Linq;

namespace ZRO06A_HFT_2021221.Repository
{
   public interface IRepository<T> where T : class
   {
      T GetOne(int id);
      IQueryable<T> GetAll();
      void Create(T item);
      void Delete(int id);
      void Update(T item);
   }
}