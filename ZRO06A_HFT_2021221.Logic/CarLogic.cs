using System;
using System.Collections.Generic;
using ZRO06A_HFT_2021221.Models;
using ZRO06A_HFT_2021221.Repository;

namespace ZRO06A_HFT_2021221.Logic
{
   public class CarLogic : ICarLogic
   {
      private readonly ICarRepository repository;

      public CarLogic(ICarRepository repository)
      {
         this.repository = repository;
      }
      
      public void Create(Car item)
      {
         if (item is null)
            throw new ArgumentNullException(nameof(item));
         if (item.BasePrice < 0)
            throw new ArgumentException("Car price cannot be negative");
         if (item.Model is null || item.Model.Equals(string.Empty))
            throw new ArgumentException("Car model is required");
         
         repository.Create(item);
      }

      public void Delete(int id)
      {
         repository.Delete(id);
      }

      public Car GetOne(int id)
      {
         Car item = repository.GetOne(id);
         if (item is null)
            throw new KeyNotFoundException("Cannot get car with id: " + id);
         
         return item;
      }

      public IEnumerable<Car> GetAll()
      {
         return repository.GetAll();
      }

      public void Update(Car item)
      {
         if (item is null)
            throw new ArgumentNullException(nameof(item));
         if (item.BasePrice < 0)
            throw new ArgumentException("Car price cannot be negative");
         if (item.Model is null || item.Model.Equals(string.Empty))
            throw new ArgumentException("Car model is required");
         if (repository.GetOne(item.Id) is null)
            throw new KeyNotFoundException("Cannot get car with id: " + item.Id);
         
         repository.Update(item);
      }
   }
}