using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.WpfClient
{
    internal class EditCarWindowViewModel
    {
        public EditCarWindowViewModel()
        {
            Car = new Car();
        }

        public EditCarWindowViewModel(Car car, IReadOnlyList<Brand>? brands)
        {
            Car = car;
            Brands = brands;
            SelectedBrand = brands?.SingleOrDefault((x) => x.Id == car.BrandId);
        }

        public Car Car { get; }
        public IReadOnlyList<Brand>? Brands { get; }
        public Brand? SelectedBrand {
            get => Car.Brand; 
            set {
                Car.Brand = value;
                if (Car.Brand != null) Car.BrandId = Car.Brand.Id;
            } 
        }

    }
}
