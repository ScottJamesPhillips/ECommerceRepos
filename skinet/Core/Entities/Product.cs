namespace Core.Entities
{
    public class Product : BaseEntity
    {
        //24. Extented class 'Product' from 'Base Entity' class just created
        //24. Remove the actual 'id', this will be stored in the BaseEntity
        //24. Added more product fields Description -> ProductBrandId
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}