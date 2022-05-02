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
    public class CarController : Controller
    {
        private readonly ICarLogic carLogic;
        private readonly IHubContext<SignalRHub> hub;

        public CarController(ICarLogic carLogic, IHubContext<SignalRHub> hub)
        {
            this.carLogic = carLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return carLogic.GetAll();
        }

        [HttpGet("{id}")]
        public Car Get(int id)
        {
            return carLogic.GetOne(id);
        }

        [HttpPost]
        public void Post([FromBody] Car value)
        {
            carLogic.Create(value);
            hub.Clients.All.SendAsync("CarCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Car value)
        {
            carLogic.Update(value);

            value = carLogic.GetOne(value.Id);
            hub.Clients.All.SendAsync("CarUpdated", value);

            foreach (Order order in value.Orders)
            {
                hub.Clients.All.SendAsync("OrderUpdated", order);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var tmp = carLogic.GetOne(id);
            carLogic.Delete(id);
            hub.Clients.All.SendAsync("CarDeleted", tmp);
        }
    }
}