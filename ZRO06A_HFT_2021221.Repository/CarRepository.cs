using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Repository
{
   public class CarRepository :
      Repository<Car>, ICarRepository
   {
      public CarRepository(DbContext ctx) : base(ctx) { }

      public override void Update(Car item)
      {
         Car oldCar = GetOne(item.Id);
         oldCar.BrandId = item.BrandId;
         oldCar.Model = item.Model;
         oldCar.BasePrice = item.BasePrice;
         ctx.SaveChanges();
      }

      public override Car GetOne(int id)
      {
         return GetAll().SingleOrDefault(x => x.Id == id);
      }

      public override void Create(Car item)
      {
         ctx.Add(item);
         ctx.SaveChanges();
      }

      public override void Delete(int id)
      {
         ctx.Remove(GetOne(id));
         ctx.SaveChanges();
      }

      public void ChangePrice(int id, int newPrice)
      {
         Car car = GetOne(id);
         if (car == null)
            throw new InvalidOperationException(
               "Car not found"
            );
         car.BasePrice = newPrice;
         // Unit of Work pattern ???
         ctx.SaveChanges();
      }
   }
}