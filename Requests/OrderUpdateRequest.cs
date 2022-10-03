using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace KitchenApi.Requests
{
    public class OrderUpdateRequest
    {
        [Min(1)]
        public int OrderNumber { get; set; }
        [MaxLength(15)]
        public string Status { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public DateTime FinishDateTime { get; set; }
    }
}
