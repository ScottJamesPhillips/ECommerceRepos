namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;

        //64. By default always return first page
        public int PageIndex {get; set;} = 1;

        private int _pageSize = 6;

        //64. This stops returning more than 50 results for request
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize)?MaxPageSize:value;
        }

        public int? BrandId {get; set;}

        public int? TypeId {get; set;}

        public string Sort {get; set;}

        
        //66 - seraching
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}