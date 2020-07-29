using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecifications : BaseSpecfication<Product>
    {
        //39. Created new spec extending from BaseSpecification type Product, had to go to BaseSpecification first and create constructor with no params first
        public ProductsWithTypesAndBrandsSpecifications(ProductSpecParams _params) :
        base(x =>
            (string.IsNullOrEmpty(_params.Search) || x.Name.ToLower().Contains(_params.Search)) &&
            (!_params.BrandId.HasValue || x.ProductBrandId == _params.BrandId) &&
            (!_params.TypeId.HasValue || x.ProductTypeId == _params.TypeId)
            )
        {
            //39. Generate this constructor, and bring in (include) producttype and productbrand, so can use this specification inside product controller
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(_params.PageSize * (_params.PageIndex - 1),
            _params.PageSize);

            //59.
            if (!string.IsNullOrEmpty(_params.Sort))
            {
                switch (_params.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;

                }
            }
        }

        //40. Create instance of constructor pssing in a param, id to search for product by id
        //also create instance base specification, passing in criteria looking for product (x) with id matching passed in id.
        public ProductsWithTypesAndBrandsSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}