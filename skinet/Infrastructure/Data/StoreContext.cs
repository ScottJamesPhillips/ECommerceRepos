using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Entities.OrderAggregate;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    //11. Add DbContext to query db away from code (Require EntityFrameworkCore)...
    public class StoreContext : DbContext
    {
        //11. Constructor for StoreContext class
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        //11. Property of type DbSet, will table of Products returned from db. Bring in class entity 'Product' (need to bring in namespace also)
        public DbSet<Product> Products { get; set; }
        //24. Add 2 new properties, 2 new tables, Products will now have foreing key pointing to 2 tables below aswell as the one above...
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        //210.
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


        //26. override method in DbContext, tell it to look for configurations file (ProductConfiguration)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            //59
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(decimal));

                        
                    //223
                    var dateTimeProperties = entityType.ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion<double>();
                    }

                    //223
                    foreach (var property in dateTimeProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}