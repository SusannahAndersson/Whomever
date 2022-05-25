using System.ComponentModel.DataAnnotations;

namespace Whomever.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public int ProductId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSize { get; set; }
        public string ProductBrand { get; set; }
        public string ProductProductId { get; set; }    
    }
}