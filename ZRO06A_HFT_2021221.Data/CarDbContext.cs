using System;
using Microsoft.EntityFrameworkCore;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Data
{
   public abstract class CarDbContext : DbContext
   {
      // Tables
      public DbSet<Brand> Brand { get; set; }
      public DbSet<Car> Cars { get; set; }
      public DbSet<Order> Orders { get; set; }
      public DbSet<Customer> Customers { get; set; }

      public CarDbContext()
      {
         // creating the necessary elements to get the database
         this.Database.EnsureDeleted();
         this.Database.EnsureCreated();
      }

      public CarDbContext(DbContextOptions<CarDbContext> options)
         : base(options) { }

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
         Brand bmw = new Brand() { Id = 1, Name = "BMW" };
         Brand citroen = new Brand() { Id = 2, Name = "Citroen" };
         Brand audi = new Brand() { Id = 3, Name = "Audi" };

         Car bmw1 = new Car() { Id = 1, BrandId = bmw.Id, BasePrice = 20000, Model = "BMW 116d" };
         Car bmw2 = new Car() { Id = 2, BrandId = bmw.Id, BasePrice = 30000, Model = "BMW 510" };
         Car citroen1 = new Car() { Id = 3, BrandId = citroen.Id, BasePrice = 10000, Model = "Citroen C1" };
         Car citroen2 = new Car() { Id = 4, BrandId = citroen.Id, BasePrice = 15000, Model = "Citroen C3" };
         Car audi1 = new Car() { Id = 5, BrandId = audi.Id, BasePrice = 20000, Model = "Audi A3" };
         Car audi2 = new Car() { Id = 6, BrandId = audi.Id, BasePrice = 25000, Model = "Audi A4" };

         Customer c1 = new Customer() { Id = 1, Name = "Roderick Myers" };
         Customer c2 = new Customer() { Id = 2, Name = "Matthew Clayton" };
         Customer c3 = new Customer() { Id = 3, Name = "Bernadette Valée" };
         
         Order o1 = new Order() { Id = 1, CustomerId = 1, CarId = 2, Date = new DateTime(2021, 11, 16), Price = 30000};
         Order o2 = new Order() { Id = 2, CustomerId = 1, CarId = 5, Date = new DateTime(2021, 11, 16), Price = 20000};
         Order o3 = new Order() { Id = 3, CustomerId = 3, CarId = 3, Date = new DateTime(2021, 11, 18), Price = 12000};
         
         modelBuilder.Entity<Brand>().HasData(bmw, citroen, audi);
         modelBuilder.Entity<Car>().HasData(bmw1, bmw2, citroen1, citroen2, audi1, audi2);
         modelBuilder.Entity<Customer>().HasData(c1, c2, c3);
         modelBuilder.Entity<Order>().HasData(o1, o2, o3);
      }
   }
}