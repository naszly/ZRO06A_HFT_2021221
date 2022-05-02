using System.Collections.Generic;

namespace ZRO06A_HFT_2021221.Logic
{
    public interface ILogic<T>
    {
        void Create(T item);
        void Delete(int id);
        T GetOne(int id);
        IEnumerable<T> GetAll();
        void Update(T item);
    }
}