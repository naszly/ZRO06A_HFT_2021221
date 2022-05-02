using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.WPFClient
{
    /// <summary>
    /// Interaction logic for EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        public EditOrderWindow()
        {
            InitializeComponent();
        }

        public EditOrderWindow(Order order, IReadOnlyList<Customer> customers, IReadOnlyList<Car> cars)
        {
            InitializeComponent();
            DataContext = new EditOrderWindowViewModel(order, customers, cars);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
