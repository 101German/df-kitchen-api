using KitchenApi.Requests;

namespace KitchenApi.DTO.Order
{
    public class OrderForReturn
    {
        public int OrderNumber { get; set; }
        public DateTime FinishDateTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
