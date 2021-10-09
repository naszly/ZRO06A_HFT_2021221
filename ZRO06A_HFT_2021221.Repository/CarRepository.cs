using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ZRO06A_HFT_2021221.Data;

namespace ZRO06A_HFT_2021221.Repository
{
   public class CarRepository :
      Repository<Car>, ICarRepository
   {
      public CarRepository(DbContext ctx) : base(ctx) {}

      public override Car GetOne(int id)
      {
         return GetAll().SingleOrDefault(x => x.Id == id);
      }

      public void ChangePrice(int id, int newPrice)
      {
         var car = GetOne(id);
         if (car == null)
         {
            throw new InvalidOperationException(
               "Car not found"
            );
         }
         car.BasePrice = newPrice;
         // Unit of Work pattern ???
         ctx.SaveChanges();
      }
   }
}