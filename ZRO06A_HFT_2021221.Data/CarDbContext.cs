using System;
using Microsoft.EntityFrameworkCore;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Data
{
   public abstract class CarDbContext : DbContext
   {
      public CarDbContext()
      {
         // creating the necessary elements to get the database
         Database.EnsureCreated();
      }

      public CarDbContext(DbContextOptions<CarDbContext> options)
         : base(options) { }

      // Tables
      public DbSet<Brand> Brand { get; set; }
      public DbSet<Car> Cars { get; set; }
      public DbSet<Order> Orders { get; set; }
      public DbSet<Customer> Customers { get; set; }

      protected abstract void OnConfiguringImp(DbContextOptionsBuilder optionsBuilder);

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         OnConfiguringImp(optionsBuilder);
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Car>(entity =>
         {
            entity.HasOne(car => car.Brand)
               .WithMany(brand => brand.Cars)
               .HasForeignKey(car => car.BrandId)
               .OnDelete(DeleteBehavior.ClientSetNull);
         });

         modelBuilder.Entity<Order>(entity =>
         {
            entity.HasOne(order => order.Car)
               .WithMany(car => car.Orders)
               .HasForeignKey(order => order.CarId)
               .OnDelete(DeleteBehavior.ClientSetNull);
         });

         modelBuilder.Entity<Order>(entity =>
         {
            entity.HasOne(order => order.Customer)
               .WithMany(customer => customer.Orders)
               .HasForeignKey(order => order.CustomerId)
               .OnDelete(DeleteBehavior.ClientSetNull);
         });

         // Part 1
         var bmw = new Brand { Id = 1, Name = "BMW" };
         var citroen = new Brand { Id = 2, Name = "Citroen" };
         var audi = new Brand { Id = 3, Name = "Audi" };

         var bmw1 = new Car { Id = 1, BrandId = bmw.Id, BasePrice = 20000, Model = "BMW 116d" };
         var bmw2 = new Car { Id = 2, BrandId = bmw.Id, BasePrice = 30000, Model = "BMW 510" };
         var citroen1 = new Car { Id = 3, BrandId = citroen.Id, BasePrice = 10000, Model = "Citroen C1" };
         var citroen2 = new Car { Id = 4, BrandId = citroen.Id, BasePrice = 15000, Model = "Citroen C3" };
         var audi1 = new Car { Id = 5, BrandId = audi.Id, BasePrice = 20000, Model = "Audi A3" };
         var audi2 = new Car { Id = 6, BrandId = audi.Id, BasePrice = 25000, Model = "Audi A4" };

         var c1 = new Customer { Id = 1, Name = "Roderick Myers" };
         var c2 = new Customer { Id = 2, Name = "Matthew Clayton" };
         var c3 = new Customer { Id = 3, Name = "Bernadette Valée" };

         var o1 = new Order { Id = 1, CustomerId = 1, CarId = 2, Date = new DateTime(2021, 11, 16), Price = 30000 };
         var o2 = new Order { Id = 2, CustomerId = 1, CarId = 5, Date = new DateTime(2021, 11, 16), Price = 20000 };
         var o3 = new Order { Id = 3, CustomerId = 3, CarId = 3, Date = new DateTime(2021, 11, 18), Price = 12000 };

         modelBuilder.Entity<Brand>().HasData(bmw, citroen, audi);
         modelBuilder.Entity<Car>().HasData(bmw1, bmw2, citroen1, citroen2, audi1, audi2);
         modelBuilder.Entity<Customer>().HasData(c1, c2, c3);
         modelBuilder.Entity<Order>().HasData(o1, o2, o3);
      }
   }
}