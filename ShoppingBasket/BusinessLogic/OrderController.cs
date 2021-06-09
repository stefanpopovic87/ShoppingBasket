using ShoppingBasket.Data;
using ShoppingBasket.Entities;
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

        public void UpdateOrder(Order order, int quantity)
        {
            decimal unitPrice = (decimal)(_context.Products.FirstOrDefault(p => p.Id == order.ProductId)?.UnitPrice);
            order.Quantity += quantity;
            order.TotalAmount = order.Quantity * unitPrice;
        }

        public void CreateOrder(Order order)
        {
            order.Id = _context.Orders.Count + 1;
            decimal unitPrice = (decimal)(_context.Products.FirstOrDefault(p => p.Id == order.ProductId)?.UnitPrice);
            order.TotalAmount = order.Quantity * unitPrice;
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
