using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZRO06A_HFT_2021221.Logic;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerLogic customerLogic;

        public CustomerController(ICustomerLogic customerLogic)
        {
            this.customerLogic = customerLogic;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return customerLogic.GetAll();
        }

        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return customerLogic.GetOne(id);
        }

        [HttpPost]
        public void Post([FromBody] Customer value)
        {
            customerLogic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Customer value)
        {
            customerLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            customerLogic.Delete(id);
        }
    }
}