using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.WpfClient
{
    internal class EditOrderWindowViewModel
    {
        public EditOrderWindowViewModel()
        {
            Order = new Order();
        }

        public EditOrderWindowViewModel(Order order, IReadOnlyList<Customer>? customers, IReadOnlyList<Car>? cars)
        {
            Order = order;
            Customers = customers;
            Cars = cars;
            SelectedCustomer = customers?.SingleOrDefault((x) => x.Id == order.CustomerId);
            SelectedCar = cars?.SingleOrDefault((x) => x.Id == order.CarId);
        }

        public Order Order { get; private set; }

        public IReadOnlyList<Customer>? Customers { get; }
        public Customer? SelectedCustomer
        {
            get => Order.Customer;
            set
            {
                Order.Customer = value;
                if (Order.Customer != null) Order.CustomerId = Order.Customer.Id;
            }
        }

        public IReadOnlyList<Car>? Cars { get; }
        public Car? SelectedCar
        {
            get => Order.Car;
            set
            {
                Order.Car = value;
                if (Order.Car != null) Order.CarId = Order.Car.Id;
            }
        }
    }
}
