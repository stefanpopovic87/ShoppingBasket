using ShoppingBasket.Data;
using ShoppingBasket.Entities;
using System.Collections.Generic;

namespace ShoppingBasket.BusinessLogic
{
    public class OrderController
    {
        public void UpdateOrder(Order orderDetails, int quantity, decimal unitPrice)
        {
            orderDetails.QuantityOrder += quantity;
            //DiscoutCalculation(shoppingCart, orderDetails);
            orderDetails.TotalAmount = orderDetails.QuantityOrder * unitPrice;
        }

        public Order CreateOrder(List<Order> shoppingCart, int productId, int quantity, decimal unitPrice)
        {
            var order = new Order
            {
                Id = shoppingCart.Count + 1,
                ProductId = productId,
                QuantityOrder = quantity,
                TotalAmount = quantity * unitPrice
            };

            shoppingCart.Add(order);
            return order;
        }
    }
}
