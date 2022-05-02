using System.Collections.Generic;
using System.Windows;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.WpfClient
{
    /// <summary>
    /// Interaction logic for EditCarWindow.xaml
    /// </summary>
    public partial class EditCarWindow : Window
    {
        public EditCarWindow()
        {
            InitializeComponent();
        }

        public EditCarWindow(Car car, IReadOnlyList<Brand> brands)
        {
            InitializeComponent();
            DataContext = new EditCarWindowViewModel(car,brands);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }

}
