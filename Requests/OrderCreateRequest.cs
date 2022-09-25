namespace KitchenApi.Requests
{
    public class OrderCreateRequest
    {
        public int OrderNumber { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();  
        public DateTime FinishDateTime { get; set; }
    }
}
