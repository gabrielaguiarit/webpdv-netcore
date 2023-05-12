namespace WebPDV.Domain.Entities
{
    public class OrderProduct
    {
        public Guid Id { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal SubTotal => Price * Quantity;
    }

    public class OrderItem
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int ProductQuantity { get; set; }

        public decimal ProductPrice { get; set; }
    }
}
