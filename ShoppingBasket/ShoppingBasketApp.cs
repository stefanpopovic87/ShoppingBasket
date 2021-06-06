using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
    public class ShoppingBasketApp
    {
        private List<OrderDetails> shoppingCart;
        private List<Product> productList;
        private List<Discount> discountList;

        public ShoppingBasketApp()
        {
            InitializeProductList();
            InitializeDiscountList();
            shoppingCart = new List<OrderDetails>();
        }

        public void Execute()
        {
            while (true)
            {
                PrintHelper.WelcomeMessage();
                string opt = Console.ReadLine();
                switch (opt)
                {
                    case "1":
                        BrowseProducts();
                        AddShoppingCart();
                        break;
                    case "2":
                        ViewShoppingCart();
                        break;
                    default:
                        PrintHelper.PrintMessage("Invalid option.", ConsoleColor.Red);
                        break;
                }
                //Console.ReadKey();
            }

        }

        private void InitializeProductList()
        {
            productList = new List<Product>()
            {
                new Product(){ Id = 1, ProductName = "Butter", UnitPrice = 0.80M},
                new Product(){ Id = 2, ProductName = "Milk", UnitPrice = 1.15M},
                new Product(){ Id = 3, ProductName = "Bread", UnitPrice = 1.00M}
            };
        }

        private void InitializeDiscountList()
        {
            discountList = new List<Discount>
            {
                new Discount{ Id = 1, Name = "Bread discount", Description = "Buy 2 butters and get one bread at 50% off", ProductConditionsId = (int)Products.Butter, ProductToDiscountId = (int)Products.Bread, Quantity = 2},
                new Discount{ Id = 2, Name = "Milk discount", Description = "Buy 3 milks and get the 4th milk for free", ProductConditionsId = (int)Products.Milk, ProductToDiscountId = (int)Products.Milk, Quantity = 3}
            };
        }

        private void BrowseProducts()
        {
            PrintHelper.PrintPoducts(productList);
        }

        private void AddShoppingCart()
        {
            Console.Write("Enter the product ID you want to buy: ");
            int productId = int.Parse(Console.ReadLine());

            var selectedProduct = productList.FirstOrDefault(p => p.Id == productId);

            if (selectedProduct == null)
            {
                PrintHelper.PrintMessage("Product not found.", ConsoleColor.Red);
                return;
            }

            Console.Write("Enter quantity to buy: ");
            int quantity = int.Parse(Console.ReadLine());

            var existingProductInShoppingCart = shoppingCart.FirstOrDefault(s => s.ProductId == productId);
           
            if (existingProductInShoppingCart != null)
            {
                existingProductInShoppingCart.QuantityOrder += quantity;
                DiscoutCalculation(shoppingCart, existingProductInShoppingCart);
                existingProductInShoppingCart.TotalAmount = existingProductInShoppingCart.QuantityOrder * selectedProduct.UnitPrice;
            }
            else
            {
                var order = new OrderDetails();
                order.Id = shoppingCart.Count + 1;
                order.ProductId = productId;
                order.QuantityOrder = quantity;
                order.TotalAmount = quantity * selectedProduct.UnitPrice;                
                shoppingCart.Add(order);
                DiscoutCalculation(shoppingCart, order);
            }
            PrintHelper.PrintMessage($"{selectedProduct.ProductName} added into shopping cart.", ConsoleColor.Yellow);
        }

        private void ViewShoppingCart()
        {
            Console.Clear();

            // inner join orderdetail and product.
            var shoppingCart = from s in this.shoppingCart
                                join p in productList on s.ProductId equals p.Id
                                select new { p.ProductName, p.UnitPrice, s.QuantityOrder, s.Discount, s.TotalAmount };
            var totalOrderAmount = 0M;
            totalOrderAmount = this.shoppingCart.Sum(s => s.TotalAmount);
            var table = new ConsoleTable("Product Name", "Price", "Quantity", "Discount", "Total");

            if (shoppingCart.ToList().Count == 0)
            {
                PrintHelper.PrintMessage("Shopping cart is empty. Go to browse products.", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("Total Items in Shopping Cart: " + shoppingCart.Count());
            foreach (var item in shoppingCart)
                table.AddRow(item.ProductName, item.UnitPrice, item.QuantityOrder, item.Discount, item.TotalAmount);

            table.Options.EnableCount = false;
            table.Write();

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Total Order Amount: " + totalOrderAmount);
            Console.WriteLine("------------------------------------------");

            Console.WriteLine("1. Clear shopping cart.");
            Console.WriteLine("2. Back.");
            Console.Write("Enter option: ");
            string opt2 = Console.ReadLine();
            switch (opt2)
            {
                case "1":
                    this.shoppingCart.Clear();
                    PrintHelper.PrintMessage("Shopping cart is empty. Go to browse products.", ConsoleColor.Yellow);
                    break;
                case "2":
                    break;
                default:
                    PrintHelper.PrintMessage("Invalid option.", ConsoleColor.Red);
                    break;
            }
        }

        public void DiscoutCalculation(List<OrderDetails> shoppingCart, OrderDetails orderDetails)
        {
            switch(orderDetails.ProductId)
            {
                case (int)Products.Bread :
                case (int)Products.Butter :
                    var buttersQut = shoppingCart.FirstOrDefault(s => s.ProductId == (int)Products.Butter)?.QuantityOrder;
                    var milkQut = shoppingCart.FirstOrDefault(s => s.ProductId == (int)Products.Milk)?.QuantityOrder;
                    var BreadQut = shoppingCart.FirstOrDefault(s => s.ProductId == (int)Products.Bread)?.QuantityOrder;
                    OrderDetails breadOrderDetails = shoppingCart.FirstOrDefault(s => s.ProductId == (int)Products.Bread);
                    breadOrderDetails.Discount = 0;
                   var remainingbuttersQut = buttersQut;

                    if (buttersQut >= 2 && BreadQut >= 1)
                    {
                        for (int i = 1; i <= BreadQut; i++)
                        {
                            if (remainingbuttersQut >= 2)
                            {
                                remainingbuttersQut -= 2;
                                breadOrderDetails.Discount += productList.FirstOrDefault(p => p.Id == (int)Products.Bread).UnitPrice / 2;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    break;

                case (int)Products.Milk :
                    break;
            }            
        }
    }
}
