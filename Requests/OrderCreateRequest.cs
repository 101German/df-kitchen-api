using System.ComponentModel.DataAnnotations;

namespace KitchenApi.Requests
{
    public class OrderCreateRequest
    {
        [Required]
        public int OrderNumber { get; set; }
        [Required]
        public List<Product> Products { get; set; } = new List<Product>();
        [Required]
        public DateTime FinishDateTime { get; set; }
    }
}
