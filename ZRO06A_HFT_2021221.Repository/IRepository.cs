using System.Linq;
using ZRO06A_HFT_2021221.Data;

namespace ZRO06A_HFT_2021221.Repository
{
   public interface IRepository<T> where T : class
   {
      T GetOne(int id);

      IQueryable<T> GetAll();

      // NOTE: not full CRUD, insert remove update TODO
   }

   public interface ICarRepository : IRepository<Car>
   {
      void ChangePrice(int id, int newPrice);
   }
}