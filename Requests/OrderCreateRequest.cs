using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace KitchenApi.Requests
{
    public class OrderCreateRequest
    {
        [Required]
        [Min(1)]
        public int OrderNumber { get; set; }
        [Required]
        public List<Product> Products { get; set; } = new List<Product>();
        [Required]
        public DateTime FinishDateTime { get; set; }
    }
}
