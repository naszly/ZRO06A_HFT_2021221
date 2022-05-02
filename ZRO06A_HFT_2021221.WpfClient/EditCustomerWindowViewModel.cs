using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.WpfClient
{
    internal class EditCustomerWindowViewModel
    {
        public EditCustomerWindowViewModel()
        {
            Customer = new Customer();
        }

        public EditCustomerWindowViewModel(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer { get; }
    }
}
