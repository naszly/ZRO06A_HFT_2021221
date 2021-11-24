using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Channels;
using ConsoleTools;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Client
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         Thread.Sleep(10000);
         RestService restService = new RestService("http://localhost:5000");

         var carMenu = new ConsoleMenu(args, level: 1)
            .Add("GetAll", () => restService.Get<Car>("car").ToConsole("Cars"))
            .Add("Get", () => restService.Get<Car>(ReadInt(), "car").ToConsole("Car"))
            .Add("Create", () => restService.Post(createObject<Car>(false), "car"))
            .Add("Update", () => restService.Put(createObject<Car>(true), "car"))
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

         var brandMenu = new ConsoleMenu(args, level: 1)
            .Add("GetAll", () => restService.Get<Brand>("brand").ToConsole("Brands"))
            .Add("Get", () => restService.Get<Brand>(ReadInt(), "brand").ToConsole("Brand"))
            .Add("Create", () => restService.Post(createObject<Brand>(false), "brand"))
            .Add("Update", () => restService.Put(createObject<Brand>(true), "brand"))
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

         var customerMenu = new ConsoleMenu(args, level: 1)
            .Add("GetAll", () => restService.Get<Customer>("customer").ToConsole("Customers"))
            .Add("Get", () => restService.Get<Customer>(ReadInt(), "customer").ToConsole("Customer"))
            .Add("Create", () => restService.Post(createObject<Customer>(false), "customer"))
            .Add("Update", () => restService.Put(createObject<Customer>(true), "customer"))
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

         var orderMenu = new ConsoleMenu(args, level: 1)
            .Add("GetAll", () => restService.Get<Order>("order").ToConsole("Orders"))
            .Add("Get", () => restService.Get<Order>(ReadInt(), "order").ToConsole("Order"))
            .Add("Create", () => restService.Post(createObject<Order>(false), "order"))
            .Add("Update", () => restService.Put(createObject<Order>(true), "order"))
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

         var menu = new ConsoleMenu(args, level: 0)
            .Add("Cars", carMenu.Show)
            .Add("Brands", brandMenu.Show)
            .Add("Customers", customerMenu.Show)
            .Add("Orders", orderMenu.Show)
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

      private static T createObject<T>(bool createId)
      {
         Type t = typeof(T);
         var props = t.GetProperties();
            var obj = Activator.CreateInstance(t);
            foreach (var prop in props)
            {
               if (!createId && prop.Name.ToUpper() == "ID") continue;
               
               var p = t.GetProperty(prop.Name);
               if (Attribute.IsDefined(p, typeof(NotMappedAttribute))) continue;

               Console.WriteLine($"{prop.Name}: ");
               var value = Console.ReadLine();
               
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
         Console.WriteLine("*** BEGIN " + str);
         JsonSerializerOptions serializerOptions = new JsonSerializerOptions { WriteIndented = true };
         Console.WriteLine(JsonSerializer.Serialize(
            input,
            serializerOptions)
         );
         Console.WriteLine("*** END " + str);
         Console.ReadLine();
      }
   }
}