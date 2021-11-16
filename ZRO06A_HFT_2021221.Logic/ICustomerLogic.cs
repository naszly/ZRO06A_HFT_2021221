using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Logic
{
   public interface ICustomerLogic : ILogic<Customer>
   {
      public int GetPaidSum(int id);

      public Order GetLastOrder(int id);
   }
}