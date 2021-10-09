using System;
using System.Runtime.Versioning;
using Microsoft.EntityFrameworkCore;

namespace ZRO06A_HFT_2021221.Data
{
   public class CarDbContext : DbContext
   {
      // Tables
      public DbSet<Brand> Brand { get; set; }

      public DbSet<Car> Cars { get; set; }
      //public DbSet<Order> Orders { get; set; }

      public CarDbContext()
      {
         // creating the necessary elements to get the database
         this.Database.EnsureCreated();
      }

      public CarDbContext(DbContextOptions<CarDbContext> options)
         : base(options) { }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            if (OperatingSystem.IsLinux()) 
            {
               optionsBuilder.UseLazyLoadingProxies().
                  UseSqlServer(
                     "Data Source=tcp:localhost;" +
                     // "User Instance=true;" +
                     "User Id=sa;" + 
                     "Password=<YourStrong@Passw0rd>;" 
                  );
            }
            else
            {
               optionsBuilder.UseLazyLoadingProxies().
                  // |DataDirectory| 
                  // must close the collection otherwise can not copy data
                  // data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\CarDb.mdf;integrated security=True;MultipleActiveResultSets=True
                  UseSqlServer(
                     @"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\CarDb.mdf;integrated security=True;MultipleActiveResultSets=True");
            }
         }
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

         /*Order o1 = new Order() { Id = 1, Name = "Roderick Myers", CarId = 2 };
         Order o2 = new Order() { Id = 2, Name = "Matthew Clayton", CarId = 5 };
         Order o3 = new Order() { Id = 3, Name = "Bernadette Valée", CarId = 3 };*/

         modelBuilder.Entity<Brand>().HasData(bmw, citroen, audi);
         modelBuilder.Entity<Car>().HasData(bmw1, bmw2, citroen1, citroen2, audi1, audi2);
         //modelBuilder.Entity<Order>().HasData(o1, o2, o3);
      }
   }
}