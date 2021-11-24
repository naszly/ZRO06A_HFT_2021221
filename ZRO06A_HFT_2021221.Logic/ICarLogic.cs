using System.Collections.Generic;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Logic
{
   public interface ICarLogic : ILogic<Car>
   {
      public double AveragePrice();
      public IEnumerable<KeyValuePair<string, double>> AveragePriceByBrands();

      public int SumSoldPrice();

      public int SumSoldPrice(int id);

      public int CountSold();

      public int CountSold(int id);

      public IEnumerable<KeyValuePair<string, double>> CountSoldByBrands();
   }
}