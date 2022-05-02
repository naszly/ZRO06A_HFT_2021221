using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZRO06A_HFT_2021221.Logic;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Endpoint
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : Controller
    {
        private readonly ICarLogic carLogic;
        private readonly ICustomerLogic customerLogic;

        public StatController(ICarLogic carLogic, ICustomerLogic customerLogic)
        {
            this.carLogic = carLogic;
            this.customerLogic = customerLogic;
        }

        // stat/CarAveragePrice
        [HttpGet]
        public double CarAveragePrice()
        {
            return carLogic.AveragePrice();
        }

        // stat/SumSoldCarPrices
        [HttpGet]
        public double SumSoldCarPrices()
        {
            return carLogic.SumSoldPrice();
        }

        // stat/CountSoldCars
        [HttpGet]
        public double CountSoldCars()
        {
            return carLogic.CountSold();
        }

        // stat/CountSoldCarsByBrand
        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> CountSoldCarsByBrand()
        {
            return carLogic.CountSoldByBrands();
        }

        // stat/CarAveragePriceByBrands
        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> CarAveragePriceByBrands()
        {
            return carLogic.AveragePriceByBrands();
        }

        // stat/CustomerPaidSum/id
        [HttpGet("{id}")]
        public double CustomerPaidSum(int id)
        {
            return customerLogic.GetPaidSum(id);
        }

        // stat/CustomerLastOrder/id
        [HttpGet("{id}")]
        public Order CustomerLastOrder(int id)
        {
            return customerLogic.GetLastOrder(id);
        }
    }
}