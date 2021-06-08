using ShoppingBasket.Data;
using ShoppingBasket.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.BusinessLogic
{
    public class OrderController
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        public void UpdateOrder(Order orderDetails, int quantity, decimal unitPrice)
        {
            orderDetails.QuantityOrder += quantity;
            orderDetails.TotalAmount = orderDetails.QuantityOrder * unitPrice;
        }

        public Order CreateOrder(int productId, int quantity, decimal unitPrice)
        {
            var order = new Order
            {
                Id = _context.Orders.Count + 1,
                ProductId = productId,
                QuantityOrder = quantity,
                TotalAmount = quantity * unitPrice
            };
            
            return order;
        }

        public decimal CalculateTotalOrderAmount()
        {
            return _context.Orders.Sum(s => s.TotalAmount);
        }

        public decimal CalculateTotalOrderDiscount()
        {
            return _context.Orders.Sum(s => s.Discount);
        }

        public decimal CalculateTotal()
        {
            return CalculateTotalOrderAmount() - CalculateTotalOrderDiscount();
        }
    }
}
