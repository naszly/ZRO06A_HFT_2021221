using Microsoft.EntityFrameworkCore;

namespace ZRO06A_HFT_2021221.Data
{
   public class MsSqlCarDbContext : CarDbContext
   {
      protected override void OnConfiguringImp(DbContextOptionsBuilder optionsBuilder)
      {
         if (optionsBuilder.IsConfigured) return;

         optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
            @"Data Source=tcp:localhost;
            Initial catalog=EFDB;
            User Id=sa;
            Password=<YourStrong@Passw0rd>;"
         );
      }
   }
}