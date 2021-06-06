using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductToDiscountId { get; set; }
        public int ProductConditionsId { get; set; }
        public int Quantity { get; set; }
        public int DiscountProcentage{ get; set; }
    }
}
