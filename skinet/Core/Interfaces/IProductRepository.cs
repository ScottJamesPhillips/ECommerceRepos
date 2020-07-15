using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        //29. currently using one repository for all types...
         Task<Product> GetProductByIDAsync(int id);
         Task<IReadOnlyList<Product>> GetProductsAsync();
         Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
         Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}