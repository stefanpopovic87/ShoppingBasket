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

        public void UpdateOrder(Order existingOrder, int quantity)
        {
            decimal unitPrice = (decimal)(_context.Products.FirstOrDefault(p => p.Id == existingOrder.ProductId)?.UnitPrice);
            existingOrder.Quantity += quantity;
            existingOrder.TotalAmount = existingOrder.Quantity * unitPrice;
        }

        public void CreateOrder(Order order)
        {
            order.Id = _context.Orders.Count + 1;
            decimal unitPrice = (decimal)(_context.Products.FirstOrDefault(p => p.Id == order.ProductId)?.UnitPrice);
            order.TotalAmount = order.Quantity * unitPrice;
        }

        public decimal CalculateTotalOrderAmount(List<Order> orders)
        {
            return orders.Sum(s => s.TotalAmount);
        }

        public decimal CalculateTotalOrderDiscount(List<Order> orders)
        {
            return orders.Sum(s => s.Discount);
        }

        public decimal CalculateTotal(List<Order> orders)
        {
            return orders.Sum(s => s.TotalAmount) - orders.Sum(s => s.Discount);
        }
    }
}
