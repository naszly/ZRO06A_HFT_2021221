using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.WPFClient
{
    internal class EditOrderWindowViewModel : ObservableRecipient
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

        private Order Order { get; set; }

        public DateTime Date
        {
            get
            {
                if (Order.Date == default)
                    Order.Date = DateTime.Now;
                return Order.Date;
            }
            set
            {
                Order.Date = value;
                OnPropertyChanged();
            }
        }

        public int Price
        {
            get => Order.Price;
            set
            {
                Order.Price = value;
                OnPropertyChanged();
            }
        }

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
                if (Order.Car != null)
                {
                    Order.CarId = Order.Car.Id;
                    Price = Order.Car.BasePrice;
                }
            }
        }
    }
}
