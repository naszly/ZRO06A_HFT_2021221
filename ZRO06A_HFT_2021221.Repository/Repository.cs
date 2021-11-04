using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZRO06A_HFT_2021221.Repository
{
   public abstract class Repository<T> :
      IRepository<T> where T : class
   {
      protected DbContext ctx;

      public Repository(DbContext ctx)
      {
         this.ctx = ctx;
      }

      public IQueryable<T> GetAll()
      {
         return ctx.Set<T>();
      }

      public abstract void Create(T item);

      public abstract void Delete(int id);

      public abstract void Update(T item);

      public abstract T GetOne(int id);
   }
}