using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ZRO06A_HFT_2021221.Logic;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Endpoint.Controllers
{
   [Route("[controller]")]
   [ApiController]
   public class CarController : Controller
   {
      private readonly ICarLogic carLogic;

      public CarController(ICarLogic carLogic)
      {
         this.carLogic = carLogic;
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
      }
      
      [HttpPut]
      public void Put([FromBody] Car value)
      {
         carLogic.Update(value);
      }
      
      [HttpDelete("{id}")]
      public void Delete(int id)
      {
         carLogic.Delete(id);
      }
   }
}