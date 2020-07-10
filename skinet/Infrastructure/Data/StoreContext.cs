using Core.Entities;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Product> Products {get; set;}
        //24. Add 2 new properties, 2 new tables, Products will now have foreing key pointing to 2 tables below aswell as the one above...
        public DbSet<ProductBrand> ProductBrands {get; set;}
        public DbSet<ProductType> ProductTypes {get; set;}

    }
}