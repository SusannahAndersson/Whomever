namespace Whomever.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderNumber { get; set; }

        //relate order entity to orderitem entity w one-to-many relationship

        public ICollection<OrderItem>? Items { get; set; }
    }
}