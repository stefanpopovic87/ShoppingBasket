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

        public void CalculateDiscout(int productId)
        {
            Discount discount = _context.Discounts.FirstOrDefault(p => p.TargetProductId == productId || p.ConditionProductId == productId);
            Order targetOrder = _context.Orders.FirstOrDefault(o => o.ProductId == discount.TargetProductId);
            Order conditionalOrder = _context.Orders.FirstOrDefault(o => o.ProductId == discount.ConditionProductId);
            var discountProcentage = discount.DiscountProcentage;
            if (discount != null && targetOrder != null && conditionalOrder != null)
            {
                int conditionalQ = conditionalOrder.Quantity / discount.ConditionalQuantity;
                int discountFrequency = targetOrder.Quantity < conditionalQ ? targetOrder.Quantity : conditionalQ;
                decimal productUnitPrice = _context.Products.FirstOrDefault(p => p.Id == productId).UnitPrice;
                targetOrder.Discount = discountFrequency * discount.DiscountProcentage * productUnitPrice;
            }
        }
    }
}
