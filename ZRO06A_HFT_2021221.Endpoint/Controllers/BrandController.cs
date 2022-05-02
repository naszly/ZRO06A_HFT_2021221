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
    public class BrandController : Controller
    {
        private readonly IBrandLogic brandLogic;
        private readonly IHubContext<SignalRHub> hub;

        public BrandController(IBrandLogic brandLogic, IHubContext<SignalRHub> hub)
        {
            this.brandLogic = brandLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Brand> Get()
        {
            return brandLogic.GetAll();
        }

        [HttpGet("{id}")]
        public Brand Get(int id)
        {
            return brandLogic.GetOne(id);
        }

        [HttpPost]
        public void Post([FromBody] Brand value)
        {
            brandLogic.Create(value);
            hub.Clients.All.SendAsync("BrandCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Brand value)
        {
            brandLogic.Update(value);
            value = brandLogic.GetOne(value.Id);
            hub.Clients.All.SendAsync("BrandUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var tmp = brandLogic.GetOne(id);
            brandLogic.Delete(id);
            hub.Clients.All.SendAsync("BrandDeleted", tmp);
        }
    }
}