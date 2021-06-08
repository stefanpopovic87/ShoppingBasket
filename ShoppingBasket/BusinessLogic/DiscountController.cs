using ShoppingBasket.Data;
using ShoppingBasket.Entities;
using System.Collections.Generic;
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

        public void CalculateDiscout(List<Order> shoppingCart, List<Discount> discountList, int productId)
        {
            Discount discount = discountList.FirstOrDefault(p => p.TargetProductId == productId || p.ConditionProductId == productId);
            Order targetOrder = shoppingCart.FirstOrDefault(o => o.ProductId == discount.TargetProductId);
            Order conditionalOrder = shoppingCart.FirstOrDefault(o => o.ProductId == discount.ConditionProductId);
            var discountProcentage = discount.DiscountProcentage;
            if (discount != null && targetOrder != null && conditionalOrder != null)
            {
                int conditionalQ = conditionalOrder.QuantityOrder / discount.ConditionalQuantity;
                int discountFrequency = targetOrder.QuantityOrder < conditionalQ ? targetOrder.QuantityOrder : conditionalQ;
                decimal productUnitPrice = _context.Products.FirstOrDefault(p => p.Id == productId).UnitPrice;
                targetOrder.Discount = discountFrequency * discount.DiscountProcentage * productUnitPrice;
            }
        }
    }
}
