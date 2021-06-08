using ShoppingBasket.Entities;
using System.Collections.Generic;
using ShoppingBasket.Enums;

namespace ShoppingBasket.Data
{
    public class DataContext
    {
        public List<Discount> Discounts;
        public List<Product> Products;
        public List<Order> Orders;

        public DataContext()
        {
            Discounts = this.InitializeDiscountList();
            Products = this.InitializeProductList();
            Orders = new List<Order>();
        }

        private List<Discount> InitializeDiscountList() => new List<Discount>
            {
                new Discount{ Id = 1, Name = "Bread discount", Description = "Buy 2 butters and get one bread at 50% off", ConditionProductId = (int)ProductsEnum.Butter, TargetProductId = (int)ProductsEnum.Bread, ConditionalQuantity = 2, DiscountProcentage = 0.50M},
                new Discount{ Id = 2, Name = "Milk discount", Description = "Buy 3 milks and get the 4th milk for free", ConditionProductId = (int)ProductsEnum.Milk, TargetProductId = (int)ProductsEnum.Milk, ConditionalQuantity = 3, DiscountProcentage = 1.00M}
            };

        private List<Product> InitializeProductList()
        {
            return new List<Product>()
            {
                new Product(){ Id = 1, ProductName = "Butter", UnitPrice = 0.80M},
                new Product(){ Id = 2, ProductName = "Milk", UnitPrice = 1.15M},
                new Product(){ Id = 3, ProductName = "Bread", UnitPrice = 1.00M}
            };
        }
    }
}
