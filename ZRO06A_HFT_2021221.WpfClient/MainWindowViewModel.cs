using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.WpfClient
{
    internal class MainWindowViewModel : ObservableRecipient
    {
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
                MainWindowLogic logic = new MainWindowLogic();

                (Cars, Customers, Orders) = logic.GetCollections();
                
                CreateCarCommand = new RelayCommand(logic.CreateCar);
                DeleteCarCommand = new RelayCommand(() => logic.DeleteCar(SelectedCar),
                                                    () => SelectedCar != null && SelectedCar.Orders.Count == 0);
                UpdateCarCommand = new RelayCommand(() => logic.UpdateCar(SelectedCar),
                                                    () => SelectedCar != null);

                CreateCustomerCommand = new RelayCommand(logic.CreateCustomer);
                DeleteCustomerCommand = new RelayCommand(() => logic.DeleteCustomer(SelectedCustomer),
                                                         () => SelectedCustomer != null && SelectedCustomer.Orders.Count == 0);
                UpdateCustomerCommand = new RelayCommand(() => logic.UpdateCustomer(SelectedCustomer),
                                                         () => SelectedCustomer != null);

                CreateOrderCommand = new RelayCommand(logic.CreateOrder);
                DeleteOrderCommand = new RelayCommand(() => logic.DeleteOrder(SelectedOrder),
                                                      () => SelectedOrder != null);
                UpdateOrderCommand = new RelayCommand(() => logic.UpdateOrder(SelectedOrder),
                                                      () => SelectedOrder != null);

            }
        }

    }
}
