using System.Linq;
using ZRO06A_HFT_2021221.Data;
using ZRO06A_HFT_2021221.Models;

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

   public interface ICarRepository : IRepository<Car>
   {
      void ChangePrice(int id, int newPrice);
   }
}