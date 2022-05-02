using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZRO06A_HFT_2021221.Logic;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandController : Controller
    {
        private readonly IBrandLogic brandLogic;

        public BrandController(IBrandLogic brandLogic)
        {
            this.brandLogic = brandLogic;
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
        }

        [HttpPut]
        public void Put([FromBody] Brand value)
        {
            brandLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            brandLogic.Delete(id);
        }
    }
}