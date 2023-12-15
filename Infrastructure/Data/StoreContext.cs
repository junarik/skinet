using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructue.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //ApplyConfigurationsFromAssemble фактично говорить нам про те, щоб піти в якись проект (assembly)
            // і взяти звідти власноруч створенну конфігурацію для створення якоїсь model.
            // GetExecutingAssembly() говорить фактично про те, щоб витягнути з Infrastructure
            // (тобто власне там де міграція і проходить) з папки Config, конфігураційні файли
            // Тому це просто треба дотримуватися conventions в написанні і розміщення своїх конфігураційних файлів
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}