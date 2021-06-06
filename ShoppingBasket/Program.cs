using System;
using System.Collections.Generic;

namespace ShoppingBasket
{
    class Program
    {
        public class ShoppingBasket
        {
            private readonly List<OrderDetails> shoppingCart;
            private static List<Product> productList;

            public ShoppingBasket()
            {
                InitializeProductList();
                shoppingCart = new List<OrderDetails>();
            }
            

            static void InitializeProductList()
            {
                productList = new List<Product>() {
                new Product(){ Id = 1, ProductName = "Butter", UnitPrice = 0.80M},
                new Product(){ Id = 2, ProductName = "Milk", UnitPrice = 1.15M},
                new Product(){ Id = 3, ProductName = "Bread", UnitPrice = 1.00M}
                };
            }

        }



        static void Main(string[] args)
        {
            var app = new ShoppingBasketApp();
            app.Execute();           
        }

        
    }
}
    


