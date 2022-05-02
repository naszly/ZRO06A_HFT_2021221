using Microsoft.EntityFrameworkCore;
using System.Linq;
using ZRO06A_HFT_2021221.Models;

namespace ZRO06A_HFT_2021221.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(DbContext ctx) : base(ctx) { }

        public override void Create(Brand item)
        {
            ctx.Add(item);
            ctx.SaveChanges();
        }

        public override void Delete(int id)
        {
            ctx.Remove(GetOne(id));
            ctx.SaveChanges();
        }

        public override void Update(Brand item)
        {
            Brand oldBrand = GetOne(item.Id);
            oldBrand.Name = item.Name;
            ctx.SaveChanges();
        }

        public override Brand GetOne(int id)
        {
            return GetAll().SingleOrDefault(x => x.Id == id);
        }
    }
}