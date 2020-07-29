using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecfication<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams _params) :
        base(x =>
        (string.IsNullOrEmpty(_params.Search) || x.Name.ToLower().Contains(_params.Search)) &&
        (!_params.BrandId.HasValue || x.ProductBrandId == _params.BrandId) &&
        (!_params.TypeId.HasValue || x.ProductTypeId == _params.TypeId))
        {
        }
    }
}