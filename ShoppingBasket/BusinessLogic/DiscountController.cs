using ShoppingBasket.Data;
using ShoppingBasket.Entities;
using System.Linq;

namespace ShoppingBasket.BusinessLogic
{
    public class DiscountController
    {
        private readonly DataContext _context;

        public DiscountController(DataContext context)
        {
            _context = context;
        }

        public decimal CalculateDiscout(DataContext context, int productId)
        {
            Discount discount = context.Discounts.FirstOrDefault(p => p.TargetProductId == productId || p.ConditionProductId == productId);
            Order targetOrder = context.Orders.FirstOrDefault(o => o.ProductId == discount.TargetProductId);
            Order conditionalOrder = context.Orders.FirstOrDefault(o => o.ProductId == discount.ConditionProductId);
            var discountProcentage = discount.DiscountProcentage;
            if (discount != null && targetOrder != null && conditionalOrder != null)
            {
                int conditionalQ = conditionalOrder.Quantity / discount.ConditionalQuantity;
                int discountFrequency = targetOrder.Quantity < conditionalQ ? targetOrder.Quantity : conditionalQ;
                decimal productUnitPrice = context.Products.FirstOrDefault(p => p.Id == targetOrder.ProductId).UnitPrice;
                targetOrder.Discount = discountFrequency * discount.DiscountProcentage * productUnitPrice;
                return targetOrder.Discount;
            }
            return 0;
        }
    }
}
