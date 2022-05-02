using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using ZRO06A_HFT_2021221.Endpoint.Services;
using ZRO06A_HFT_2021221.Logic;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerLogic customerLogic;
        private readonly IHubContext<SignalRHub> hub;

        public CustomerController(ICustomerLogic customerLogic, IHubContext<SignalRHub> hub)
        {
            this.customerLogic = customerLogic;
            this.hub = hub;
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
            hub.Clients.All.SendAsync("CustomerCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Customer value)
        {
            customerLogic.Update(value);

            value = customerLogic.GetOne(value.Id);
            hub.Clients.All.SendAsync("CustomerUpdated", value);

            foreach (Order order in value.Orders)
            {
                hub.Clients.All.SendAsync("OrderUpdated", order);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var tmp = customerLogic.GetOne(id);
            customerLogic.Delete(id);
            hub.Clients.All.SendAsync("CustomerDeleted", tmp);
        }
    }
}