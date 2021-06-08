namespace ShoppingBasket.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TargetProductId { get; set; }
        public int ConditionProductId { get; set; }
        public int ConditionalQuantity { get; set; }
        public decimal DiscountProcentage{ get; set; }
    }
}
