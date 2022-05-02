using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.WpfClient
{
    internal class MainWindowLogic
    {
        private RestCollection<Brand> Brands { get; set; }
        private RestCollection<Car> Cars { get; set; }
        private RestCollection<Customer> Customers { get; set; }
        private RestCollection<Order> Orders { get; set; }

        public MainWindowLogic()
        {
            Brands = new RestCollection<Brand>("http://localhost:62730/", "brand", "hub");
            Cars = new RestCollection<Car>("http://localhost:62730/", "car", "hub");
            Customers = new RestCollection<Customer>("http://localhost:62730/", "customer", "hub");
            Orders = new RestCollection<Order>("http://localhost:62730/", "order", "hub");
        }

        public (RestCollection<Car>, RestCollection<Customer>, RestCollection<Order>) GetCollections()
        {
            return (Cars, Customers, Orders);
        }

        public void CreateCar()
        {
            Car car = new Car();
            EditCarWindow window = new EditCarWindow(car, Brands.ToList());
            if (window.ShowDialog() == true)
            {
                Cars.Add(car);
            }
        }

        public void DeleteCar(Car selectedCar)
        {
            Cars.Delete(selectedCar.Id);
        }

        public void UpdateCar(Car selectedCar)
        {
            Car car = selectedCar.GetCopy();
            EditCarWindow window = new EditCarWindow(car, Brands.ToList());
            if (window.ShowDialog() == true)
            {
                Cars.Update(car);
            }
        }

        public void CreateCustomer()
        {
            Customer customer = new Customer();
            EditCustomerWindow window = new EditCustomerWindow(customer);
            if (window.ShowDialog() == true)
            {
                Customers.Add(customer);
            }
        }

        public void DeleteCustomer(Customer selectedCustomer)
        {
            Customers.Delete(selectedCustomer.Id);
        }

        public void UpdateCustomer(Customer selectedCustomer)
        {
            Customer customer = selectedCustomer.GetCopy();
            EditCustomerWindow window = new EditCustomerWindow(customer);
            if (window.ShowDialog() == true)
            {
                Customers.Update(customer);
            }
        }

        public void CreateOrder()
        {
            Order order = new Order();
            EditOrderWindow window = new EditOrderWindow(order, Customers.ToList(), Cars.ToList());
            if (window.ShowDialog() == true)
            {
                Orders.Add(order);
            }
        }

        public void DeleteOrder(Order selectedOrder)
        {
            Orders.Delete(selectedOrder.Id);
        }

        public void UpdateOrder(Order selectedOrder)
        {
            Order order = selectedOrder.GetCopy();
            EditOrderWindow window = new EditOrderWindow(order, Customers.ToList(), Cars.ToList());
            if (window.ShowDialog() == true)
            {
                Orders.Update(order);
            }
        }
    }
}
