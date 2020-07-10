using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        //23. Generate ProductRepository constructor, initialize field from paramtetr
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        //23. populate method to async find products and print out as list read only
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        //23. populate method to async find product by id
        public async Task<Product> GetProductByIDAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}