using System;
using System.Collections.Generic;
using ZRO06A_HFT_2021221.Models;
using ZRO06A_HFT_2021221.Repository;

namespace ZRO06A_HFT_2021221.Logic
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderRepository repository;

        public OrderLogic(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public void Create(Order item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (item.Customer != null)
            {
                if (item.CustomerId == 0 && item.Customer.Id > 0)
                    item.CustomerId = item.Customer.Id;
                //item.Customer = null;
            }

            if (item.Car != null)
            {
                if (item.CarId == 0 && item.Car.Id > 0)
                    item.CarId = item.Car.Id;
                //item.Car = null;
            }

            repository.Create(item.GetCopy());
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Order GetOne(int id)
        {
            Order item = repository.GetOne(id);
            if (item is null)
                throw new KeyNotFoundException("Cannot get order with id: " + id);

            return item;
        }

        public IEnumerable<Order> GetAll()
        {
            return repository.GetAll();
        }

        public void Update(Order item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (repository.GetOne(item.Id) is null)
                throw new KeyNotFoundException("Cannot get order with id: " + item.Id);

            repository.Update(item);
        }
    }
}