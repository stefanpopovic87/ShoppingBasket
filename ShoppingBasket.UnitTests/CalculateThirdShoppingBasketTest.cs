using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasket.Data;
using ShoppingBasket.Entities;
using ShoppingBasket.Enums;
using ShoppingBasket.BusinessLogic;
using System.Linq;

namespace ShoppingBasket.UnitTests

{
    [TestClass]
    public class CalculateThirdShoppingBasketTest
    {
        [TestMethod]
        public void CalculateTotalOrderAmount_ThirdShoppingBasket_ReturnsTrue()
        {
            DataContext dataContext = new DataContext();
            DiscountController discountController = new DiscountController(dataContext);
            dataContext.Orders.Add(new Order
            {
                Id = 1,
                ProductId = (int)ProductsEnum.Milk,
                Quantity = 4,
                TotalAmount = dataContext.Products.FirstOrDefault(p => p.Id == (int)ProductsEnum.Milk).UnitPrice * 4,
                Discount = 0
            });
            OrderController orderController = new OrderController(dataContext);

            foreach (Order order in dataContext.Orders)
            {
                discountController.CalculateDiscout(dataContext, order.ProductId);
            }

            //Act
            decimal resoult = orderController.CalculateTotal(dataContext.Orders);

            // Assert
            Assert.AreEqual(3.45M, resoult);
        }
    }
}
