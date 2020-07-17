using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecifications : BaseSpecfication<Product>
    {
        //39. Created new spec extending from BaseSpecification type Product, had to go to BaseSpecification first and create constructor with no params first
        public ProductsWithTypesAndBrandsSpecifications()
        {
            //39. Generate this constructor, and bring in (include) producttype and productbrand, so can use this specification inside product controller
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
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