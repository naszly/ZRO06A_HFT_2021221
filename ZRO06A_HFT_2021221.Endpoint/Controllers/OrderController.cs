using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ZRO06A_HFT_2021221.Logic;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Endpoint.Controllers
{
   [Route("[controller]")]
   [ApiController]
   public class OrderController : Controller
   {
      private readonly IOrderLogic orderLogic;

      public OrderController(IOrderLogic orderLogic)
      {
         this.orderLogic = orderLogic;
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
      }

      [HttpPut]
      public void Put([FromBody] Order value)
      {
         orderLogic.Update(value);
      }

      [HttpDelete("{id}")]
      public void Delete(int id)
      {
         orderLogic.Delete(id);
      }
   }
}