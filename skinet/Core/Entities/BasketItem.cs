namespace Core.Entities
{
    public class BasketItem
    {
        public int Id {get; set;}
        public string ProcductName {get; set;}
        public decimal Price {get; set;}
        public int Quantity {get; set;}
        public string PictureURL {get; set;}
        public string Brand {get; set;}
        public string Type {get; set;}
    }
}