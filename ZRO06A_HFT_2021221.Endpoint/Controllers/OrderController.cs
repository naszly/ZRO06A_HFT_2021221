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
    public class OrderController : Controller
    {
        private readonly IOrderLogic orderLogic;
        private readonly IHubContext<SignalRHub> hub;

        public OrderController(IOrderLogic orderLogic, IHubContext<SignalRHub> hub)
        {
            this.orderLogic = orderLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return orderLogic.GetAll();
        }

        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return orderLogic.GetOne(id);
        }

        [HttpPost]
        public void Post([FromBody] Order value)
        {
            orderLogic.Create(value);

            hub.Clients.All.SendAsync("OrderCreated", value);

            hub.Clients.All.SendAsync("CustomerUpdated", value.Customer);
            hub.Clients.All.SendAsync("CarUpdated", value.Car);
        }

        [HttpPut]
        public void Put([FromBody] Order value)
        {
            var old = orderLogic.GetOne(value.Id);

            orderLogic.Update(value);
            value  = orderLogic.GetOne(value.Id);
            hub.Clients.All.SendAsync("OrderUpdated", value);

            hub.Clients.All.SendAsync("CustomerUpdated", old.Customer);
            hub.Clients.All.SendAsync("CustomerUpdated", value.Customer);
            hub.Clients.All.SendAsync("CarUpdated", old.Car);
            hub.Clients.All.SendAsync("CarUpdated", value.Car);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var old = orderLogic.GetOne(id);

            orderLogic.Delete(id);
            hub.Clients.All.SendAsync("OrderDeleted", old);

            hub.Clients.All.SendAsync("CustomerUpdated", old.Customer);
            hub.Clients.All.SendAsync("CarUpdated", old.Car);
        }
    }
}