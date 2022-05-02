using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            oldOrder.CustomerId = item.CustomerId;
            oldOrder.CarId = item.CarId;
            oldOrder.Price = item.Price;
            oldOrder.Date = item.Date;
            ctx.SaveChanges();
        }

        public override Order GetOne(int id)
        {
            return GetAll().SingleOrDefault(x => x.Id == id);
        }
    }
}