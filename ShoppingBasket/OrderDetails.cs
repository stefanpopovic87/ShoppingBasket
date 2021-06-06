namespace ShoppingBasket
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityOrder { get; set; }
        public decimal TotalAmount { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
    }
}
