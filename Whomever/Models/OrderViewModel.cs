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

        //┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓
        //nullable ? --> enables postman post new order for jwt applicationuser with empty array
        //not to be removed yet xD
        public ICollection<OrderItemViewModel>? Items { get; set; }

        //not to be removed yet xD
        //nullable ? --> enables postman post new order for jwt applicationuser with empty array
        //┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓┏━✦❘༻༺❘✦━━┓
    }
}