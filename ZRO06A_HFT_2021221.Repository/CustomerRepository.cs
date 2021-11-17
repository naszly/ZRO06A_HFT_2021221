using System.Linq;
using Microsoft.EntityFrameworkCore;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Repository
{
   public class CustomerRepository : Repository<Customer>, ICustomerRepository
   {
      public CustomerRepository(DbContext ctx) : base(ctx) { }

      public override void Create(Customer item)
      {
         ctx.Add(item);
         ctx.SaveChanges();
      }

      public override void Delete(int id)
      {
         ctx.Remove(GetOne(id));
         ctx.SaveChanges();
      }

      public override void Update(Customer item)
      {
         Customer oldCustomer = GetOne(item.Id);
         oldCustomer.Name = item.Name;
         ctx.SaveChanges();
      }

      public override Customer GetOne(int id)
      {
         return GetAll().SingleOrDefault(x => x.Id == id);
      }
   }
}