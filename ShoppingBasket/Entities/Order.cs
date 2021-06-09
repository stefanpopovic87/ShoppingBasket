namespace ShoppingBasket.Entities
{
    public class Order
    {
        public int Id { get; set; } // ne
        public int ProductId { get; set; } // da
        public int Quantity { get; set; } // da
        public decimal TotalAmount { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
    }
}
