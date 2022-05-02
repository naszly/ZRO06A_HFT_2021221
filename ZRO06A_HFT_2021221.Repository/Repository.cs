using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ZRO06A_HFT_2021221.Repository
{
    public abstract class Repository<T> :
      IRepository<T> where T : class
    {
        protected readonly DbContext ctx;

        protected Repository(DbContext ctx)
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