using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasket.Data;
using ShoppingBasket.Entities;
using ShoppingBasket.Enums;
using ShoppingBasket.BusinessLogic;
using System.Linq;

namespace ShoppingBasket.UnitTests

{
    [TestClass]
    public class CalculateFirstShoppingBasketTest
    {
        [TestMethod]
        public void CalculateTotalOrderAmount_FirstShoppingBasket_ReturnsTrue()
        {
            DataContext dataContext = new DataContext();
            DiscountController discountController = new DiscountController(dataContext);
            dataContext.Orders.Add(new Order
            {
                Id = 1,
                ProductId = (int)ProductsEnum.Butter,
                Quantity = 1,
                TotalAmount = dataContext.Products.FirstOrDefault(p => p.Id == (int)ProductsEnum.Butter).UnitPrice * 1,
                Discount = 0
            });
            dataContext.Orders.Add(new Order
            {
                Id = 2,
                ProductId = (int)ProductsEnum.Milk,
                Quantity = 1,
                TotalAmount = dataContext.Products.FirstOrDefault(p => p.Id == (int)ProductsEnum.Milk).UnitPrice * 1,
                Discount = 0
            });
            dataContext.Orders.Add(new Order
            {
                Id = 3,
                ProductId = (int)ProductsEnum.Bread,
                Quantity = 1,
                TotalAmount = dataContext.Products.FirstOrDefault(p => p.Id == (int)ProductsEnum.Bread).UnitPrice * 1,
                Discount = 0
            });
            OrderController orderController = new OrderController(dataContext);
            foreach (Order order in dataContext.Orders)
            {
                discountController.CalculateDiscout(dataContext, order.ProductId);
            }

            decimal resoult = orderController.CalculateTotal(dataContext.Orders);

            Assert.AreEqual(2.95M, resoult);
        }        
    }
}
