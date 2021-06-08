namespace ShoppingBasket.Models
{
    public class ShoppingCartModel
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityOrder { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
