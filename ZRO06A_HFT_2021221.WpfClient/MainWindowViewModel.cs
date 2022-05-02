using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.WpfClient
{
    internal class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Brand> Brands { get; set; }
        public RestCollection<Car> Cars { get; set; }
        public RestCollection<Customer> Customers { get; set; }
        public RestCollection<Order> Orders { get; set; }

        private Car selectedCar;
        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                SetProperty(ref selectedCar, value);
                ((RelayCommand)DeleteCarCommand).NotifyCanExecuteChanged();
                ((RelayCommand)UpdateCarCommand).NotifyCanExecuteChanged();
            }
        }

        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                SetProperty(ref selectedCustomer, value);
                ((RelayCommand)DeleteCustomerCommand).NotifyCanExecuteChanged();
                ((RelayCommand)UpdateCustomerCommand).NotifyCanExecuteChanged();
            }
        }

        private Order selectedOrder;
        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                SetProperty(ref selectedOrder, value);
                ((RelayCommand)DeleteOrderCommand).NotifyCanExecuteChanged();
                ((RelayCommand)UpdateOrderCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateCarCommand { get; set; }
        public ICommand DeleteCarCommand { get; set; }
        public ICommand UpdateCarCommand { get; set; }

        public ICommand CreateCustomerCommand { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }
        public ICommand UpdateCustomerCommand { get; set; }

        public ICommand CreateOrderCommand { get; set; }
        public ICommand DeleteOrderCommand { get; set; }
        public ICommand UpdateOrderCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Brands = new RestCollection<Brand>("http://localhost:62730/", "brand");
                Cars = new RestCollection<Car>("http://localhost:62730/", "car");
                Customers = new RestCollection<Customer>("http://localhost:62730/", "customer");
                Orders = new RestCollection<Order>("http://localhost:62730/", "order");

                CreateCarCommand = new RelayCommand(CreateCar);
                DeleteCarCommand = new RelayCommand(() => Cars.Delete(SelectedCar.Id),
                                                    () => SelectedCar != null && SelectedCar.Orders.Count == 0);
                UpdateCarCommand = new RelayCommand(UpdateCar,
                                                    () => SelectedCar != null);

                CreateCustomerCommand = new RelayCommand(CreateCustomer);
                DeleteCustomerCommand = new RelayCommand(() => Customers.Delete(SelectedCustomer.Id),
                                                         () => SelectedCustomer != null && SelectedCustomer.Orders.Count == 0);
                UpdateCustomerCommand = new RelayCommand(UpdateCustomer,
                                                         () => SelectedCustomer != null);

                CreateOrderCommand = new RelayCommand(CreateOrder);
                DeleteOrderCommand = new RelayCommand(() => Orders.Delete(SelectedOrder.Id),
                                                      () => SelectedOrder != null);
                UpdateOrderCommand = new RelayCommand(UpdateOrder,
                                                      () => SelectedOrder != null);

            }
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

        public void UpdateCar()
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

        public void UpdateCustomer()
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

        public void UpdateOrder()
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
