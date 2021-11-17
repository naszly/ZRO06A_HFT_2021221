using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZRO06A_HFT_2021221.Data;
using ZRO06A_HFT_2021221.Logic;
using ZRO06A_HFT_2021221.Repository;

namespace ZRO06A_HFT_2021221.Endpoint
{
   public class Startup
   {
      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers();

         services.AddTransient<ICarLogic, CarLogic>();
         //services.AddTransient<IBrandLogic, BrandLogic>();
         //services.AddTransient<IOrderLogic, OrderLogic>();
         //services.AddTransient<ICustomerLogic, CustomerLogic>();
         
         services.AddTransient<ICarRepository, CarRepository>();
         //services.AddTransient<IBrandRepository, BrandRepository>();
         //services.AddTransient<IOrderRepository, OrderRepository>();
         //services.AddTransient<ICustomerLogic, CustomerLogic>();
         
         if (OperatingSystem.IsWindows())
            services.AddTransient<DbContext, LocalDbCarDbContext>();
         else
            services.AddTransient<DbContext, MsSqlCarDbContext>();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseRouting();

         app.UseEndpoints(endpoints =>
         {
            //endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            endpoints.MapControllers();
         });
      }
   }
}