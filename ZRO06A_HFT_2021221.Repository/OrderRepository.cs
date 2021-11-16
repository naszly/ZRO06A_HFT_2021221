using System.Linq;
using Microsoft.EntityFrameworkCore;

using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Repository
{
   public class OrderRepository : Repository<Order>, IOrderRepository
   {
      public OrderRepository(DbContext ctx) : base(ctx) { }
      public override void Create(Order item)
      {
         ctx.Add(item);
         ctx.SaveChanges();
      }

      public override void Delete(int id)
      {
         ctx.Remove(GetOne(id));
         ctx.SaveChanges();
      }

      public override void Update(Order item)
      {
         Order oldOrder = GetOne(item.Id);
         oldOrder.Name = item.Name;
         oldOrder.CarId = item.CarId;
         ctx.SaveChanges();
      }

      public override Order GetOne(int id)
      {
         return GetAll().SingleOrDefault(x => x.Id == id);
      }
   }
}