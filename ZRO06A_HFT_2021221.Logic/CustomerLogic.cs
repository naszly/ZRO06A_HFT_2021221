using System;
using System.Collections.Generic;
using System.Linq;
using ZRO06A_HFT_2021221.Models;
using ZRO06A_HFT_2021221.Repository;

namespace ZRO06A_HFT_2021221.Logic
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerRepository repository;

        public CustomerLogic(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        public void Create(Customer item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (item.Name is null || item.Name.Equals(string.Empty))
                throw new ArgumentException("Customer name is required");

            repository.Create(item);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Customer GetOne(int id)
        {
            Customer item = repository.GetOne(id);
            if (item is null)
                throw new KeyNotFoundException("Cannot get customer with id: " + id);

            return item;
        }

        public IEnumerable<Customer> GetAll()
        {
            return repository.GetAll();
        }

        public void Update(Customer item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (item.Name is null || item.Name.Equals(string.Empty))
                throw new ArgumentException("Customer name is required");
            if (repository.GetOne(item.Id) is null)
                throw new KeyNotFoundException("Cannot get car with id: " + item.Id);

            repository.Update(item);
        }

        public int GetPaidSum(int id)
        {
            return GetOne(id).Orders.Sum(x => x.Price);
        }

        public Order GetLastOrder(int id)
        {
            return GetOne(id).Orders.OrderByDescending(x => x.Date).Take(1).SingleOrDefault();
        }

        public string FavoriteBrand(int id)
        {
            return GetOne(id).Orders.GroupBy(x => x.Car.Brand.Name)
               .Select(g => new KeyValuePair<string, int>(g.Key, g.Count()))
               .OrderByDescending(x => x.Value)
               .FirstOrDefault().Key;
        }
    }
}