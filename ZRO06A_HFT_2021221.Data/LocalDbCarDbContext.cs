using Microsoft.EntityFrameworkCore;

namespace ZRO06A_HFT_2021221.Data
{
    public class LocalDbCarDbContext : CarDbContext
    {
        protected override void OnConfiguringImp(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            optionsBuilder.UseLazyLoadingProxies().
               // |DataDirectory| 
               // must close the collection otherwise can not copy data
               // data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\CarDb.mdf;integrated security=True;MultipleActiveResultSets=True
               UseSqlServer(
                  @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CarDb.mdf;Integrated Security=True");
        }
    }
}