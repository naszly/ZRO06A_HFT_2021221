using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using ConsoleTools;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Client
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         var restService = new RestService("http://localhost:5000");

         ConsoleMenu carMenu = new ConsoleMenu(args, 1)
            .Add("GetAll", () => restService.Get<Car>("car").ToConsole("Cars"))
            .Add("Get", () => restService.Get<Car>(ReadInt(), "car").ToConsole("Car"))
            .Add("Create", () => restService.Post(CreateObject<Car>(false), "car"))
            .Add("Update", () => restService.Put(CreateObject<Car>(true), "car"))
            .Add("Delete", () => restService.Delete(ReadInt(), "car"))
            .Add("Back", ConsoleMenu.Close)
            .Configure(config =>
            {
               config.Selector = "--> ";
               config.EnableFilter = true;
               config.Title = "Cars";
               config.EnableBreadcrumb = true;
               config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

         ConsoleMenu brandMenu = new ConsoleMenu(args, 1)
            .Add("GetAll", () => restService.Get<Brand>("brand").ToConsole("Brands"))
            .Add("Get", () => restService.Get<Brand>(ReadInt(), "brand").ToConsole("Brand"))
            .Add("Create", () => restService.Post(CreateObject<Brand>(false), "brand"))
            .Add("Update", () => restService.Put(CreateObject<Brand>(true), "brand"))
            .Add("Delete", () => restService.Delete(ReadInt(), "brand"))
            .Add("Back", ConsoleMenu.Close)
            .Configure(config =>
            {
               config.Selector = "--> ";
               config.EnableFilter = true;
               config.Title = "Brands";
               config.EnableBreadcrumb = true;
               config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

         ConsoleMenu customerMenu = new ConsoleMenu(args, 1)
            .Add("GetAll", () => restService.Get<Customer>("customer").ToConsole("Customers"))
            .Add("Get", () => restService.Get<Customer>(ReadInt(), "customer").ToConsole("Customer"))
            .Add("Create", () => restService.Post(CreateObject<Customer>(false), "customer"))
            .Add("Update", () => restService.Put(CreateObject<Customer>(true), "customer"))
            .Add("Delete", () => restService.Delete(ReadInt(), "customer"))
            .Add("Back", ConsoleMenu.Close)
            .Configure(config =>
            {
               config.Selector = "--> ";
               config.EnableFilter = true;
               config.Title = "Customers";
               config.EnableBreadcrumb = true;
               config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

         ConsoleMenu orderMenu = new ConsoleMenu(args, 1)
            .Add("GetAll", () => restService.Get<Order>("order").ToConsole("Orders"))
            .Add("Get", () => restService.Get<Order>(ReadInt(), "order").ToConsole("Order"))
            .Add("Create", () => restService.Post(CreateObject<Order>(false), "order"))
            .Add("Update", () => restService.Put(CreateObject<Order>(true), "order"))
            .Add("Delete", () => restService.Delete(ReadInt(), "order"))
            .Add("Back", ConsoleMenu.Close)
            .Configure(config =>
            {
               config.Selector = "--> ";
               config.EnableFilter = true;
               config.Title = "Orders";
               config.EnableBreadcrumb = true;
               config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });
         
         ConsoleMenu statMenu = new ConsoleMenu(args, 1)
            .Add("Cars average price", () => restService.GetSingle<double>( "stat/CarAveragePrice").ToConsole("Cars average price"))
            .Add("Sum sold cars", () => restService.GetSingle<double>( "stat/SumSoldCarPrices").ToConsole("Sum sold cars"))
            .Add("Count sold cars", () => restService.GetSingle<double>( "stat/CountSoldCars").ToConsole("Count sold cars"))
            .Add("Count sold cars by brands", () => restService.Get<KeyValuePair<string, double>>( "stat/CountSoldCarsByBrand").ToConsole("Count sold cars by brands"))
            .Add("Cars average price by brands", () => restService.Get<KeyValuePair<string, double>>( "stat/CarAveragePriceByBrands").ToConsole("Cars average price by brands"))
            .Add("Customer paid sum", () => restService.Get<double>(ReadInt(), "stat/CustomerPaidSum").ToConsole("Customer paid sum"))
            .Add("Customer last order", () => restService.Get<Order>(ReadInt(), "stat/CustomerLastOrder").ToConsole("Customer last order"))
            
            .Add("Back", ConsoleMenu.Close)
            .Configure(config =>
            {
               config.Selector = "--> ";
               config.EnableFilter = true;
               config.Title = "Orders";
               config.EnableBreadcrumb = true;
               config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

         ConsoleMenu menu = new ConsoleMenu(args, 0)
            .Add("Cars", carMenu.Show)
            .Add("Brands", brandMenu.Show)
            .Add("Customers", customerMenu.Show)
            .Add("Orders", orderMenu.Show)
            .Add("Stats", statMenu.Show)
            .Add("Exit", () => Environment.Exit(0))
            .Configure(config =>
            {
               config.Selector = "--> ";
               config.EnableFilter = true;
               config.Title = "Main menu";
               config.EnableWriteTitle = true;
               config.EnableBreadcrumb = true;
            });

         menu.Show();
      }

      private static T CreateObject<T>(bool createId)
      {
         Type t = typeof(T);
         PropertyInfo[] props = t.GetProperties();
         object obj = Activator.CreateInstance(t);
         
         Console.Clear();
         Console.WriteLine($"Create {nameof(obj)}");
         foreach (PropertyInfo prop in props)
         {
            if (!createId && prop.Name.ToUpper() == "ID") continue;

            PropertyInfo p = t.GetProperty(prop.Name);
            if (Attribute.IsDefined(p, typeof(NotMappedAttribute))) continue;

            Console.WriteLine($"{prop.Name}: ");
            string value = Console.ReadLine();
            
            if (value == string.Empty) continue;
            
            p.SetValue(obj, Convert.ChangeType(value, p.PropertyType), null);
         }

         return (T)obj;
      }

      private static int ReadInt(string str = "ID")
      {
         Console.Write($"{str}: ");
         return Convert.ToInt32(Console.ReadLine());
      }

      private static void ToConsole<T>(this T input, string str = "")
      {
         Console.Clear();
         Console.WriteLine("*** BEGIN " + str);
         var serializerOptions = new JsonSerializerOptions
         {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
         };
         Console.WriteLine(JsonSerializer.Serialize(
            input,
            serializerOptions)
         );
         Console.WriteLine("*** END " + str);
         Console.ReadLine();
      }
   }
}