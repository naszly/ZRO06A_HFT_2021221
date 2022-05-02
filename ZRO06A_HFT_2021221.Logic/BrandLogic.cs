using System;
using System.Collections.Generic;
using ZRO06A_HFT_2021221.Models;
using ZRO06A_HFT_2021221.Repository;

namespace ZRO06A_HFT_2021221.Logic
{
    public class BrandLogic : IBrandLogic
    {
        private readonly IBrandRepository repository;

        public BrandLogic(IBrandRepository repository)
        {
            this.repository = repository;
        }

        public void Create(Brand item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (item.Name is null || item.Name.Equals(string.Empty))
                throw new ArgumentException("Model name is required");

            repository.Create(item);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Brand GetOne(int id)
        {
            Brand item = repository.GetOne(id);
            if (item is null)
                throw new KeyNotFoundException("Cannot get brand with id: " + id);

            return item;
        }

        public IEnumerable<Brand> GetAll()
        {
            return repository.GetAll();
        }

        public void Update(Brand item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (item.Name is null || item.Name.Equals(string.Empty))
                throw new ArgumentException("Model name is required");
            if (repository.GetOne(item.Id) is null)
                throw new KeyNotFoundException("Cannot get brand with id: " + item.Id);

            repository.Update(item);
        }
    }
}