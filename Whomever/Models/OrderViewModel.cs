using System.ComponentModel.DataAnnotations;

namespace Whomever.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        //to fix null
        [Required]
        [MinLength(1)]
        public string OrderNumber { get; set; }
    }
}