using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZRO06A_HFT_2021221.Data;
using ZRO06A_HFT_2021221.Repository;

namespace ZRO06A_HFT_2021221.Endpoint
{
   public class Program
   {
      public static void Main(string[] args)
      {
         DbContext ctx = new CarDbContext();
         var carRepository = new CarRepository(ctx);

         CreateHostBuilder(args).Build().Run();
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
   }
}