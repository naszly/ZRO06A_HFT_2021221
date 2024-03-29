using System;
using System.Collections.Generic;
using System.Linq;
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

            if (item.Brand != null)
            {
                if (item.BrandId == 0 && item.Brand.Id > 0)
                    item.BrandId = item.Brand.Id;
                //item.Brand = null;
            }

            repository.Create(item.GetCopy());
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

        public double AveragePrice()
        {
            return repository.GetAll().Average(x => x.BasePrice);
        }

        public IEnumerable<KeyValuePair<string, double>> AveragePriceByBrands()
        {
            return repository.GetAll()
               .GroupBy(x => x.Brand.Name)
               .Select(g =>
                  new KeyValuePair<string, double>(g.Key, g.Average(x => x.BasePrice))
               );
        }

        public int SumSoldPrice()
        {
            return repository.GetAll().ToArray().Select(x => x.Orders.Sum(y => y.Price)).Sum();
        }

        public int SumSoldPrice(int id)
        {
            return repository.GetOne(id).Orders.Sum(x => x.Price);
        }

        public int CountSold()
        {
            return repository.GetAll().ToArray().Select(x => x.Orders.Count).Sum();
        }

        public int CountSold(int id)
        {
            return repository.GetOne(id).Orders.Count;
        }

        public IEnumerable<KeyValuePair<string, double>> CountSoldByBrands()
        {
            return repository.GetAll().ToArray()
               .GroupBy(x => x.Brand.Name)
               .Select(g =>
                  new KeyValuePair<string, double>(g.Key, g.Sum(x => x.Orders.Count))
               );
        }

        public void ChangePrice(int id, int newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("Car price cannot be negative");
            if (repository.GetOne(id) is null)
                throw new KeyNotFoundException("Cannot get car with id: " + id);
            repository.ChangePrice(id, newPrice);
        }
    }
}