using ShoppingBasket.BusinessLogic;
using ShoppingBasket.Data;
using ShoppingBasket.Helpers;
using ShoppingBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.App
{
    public class ShoppingBasketApp
    {
        private readonly DataContext _context;
        private OrderController OrderController => new OrderController(_context);
        private DiscountController DiscountController => new DiscountController(_context);
        public ShoppingBasketApp(DataContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            while (true)
            {
                PrintHelper.WelcomeMessage();
                int opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                        BrowseProducts();
                        AddShoppingCart();
                        break;
                    case 2:
                        ViewShoppingCart();
                        break;
                    default:
                        PrintHelper.InvalidOption();
                        break;
                }
                Console.ReadKey();
            }

        }

        private void BrowseProducts()
        {
            PrintHelper.PrintPoducts(_context.Products);
        }

        private void AddShoppingCart()
        {
            PrintHelper.EnterProductId();
            int productId = int.Parse(Console.ReadLine());

            var selectedProduct = _context.Products.FirstOrDefault(p => p.Id == productId);

            if (selectedProduct == null)
            {
                PrintHelper.ProductNotFound();
                return;
            }

            PrintHelper.EnterQuantity();
            int quantity = int.Parse(Console.ReadLine());

            var existingProductInShoppingCart = _context.Orders.FirstOrDefault(s => s.ProductId == productId);

            if (existingProductInShoppingCart != null)
            {
                OrderController.UpdateOrder(existingProductInShoppingCart, quantity, selectedProduct.UnitPrice);
            }

            else
            {
                var order = OrderController.CreateOrder(productId, quantity, selectedProduct.UnitPrice);
                _context.Orders.Add(order);

            }

            DiscountController.CalculateDiscout(productId);
            PrintHelper.AddedIntoShoppingCart(selectedProduct.ProductName);
        }

        private void ViewShoppingCart()
        {
            Console.Clear();

            IEnumerable<ShoppingCartModel> shoppingCart = from s in _context.Orders
                                                          join p in _context.Products on s.ProductId equals p.Id
                                                          select new ShoppingCartModel { ProductName = p.ProductName, UnitPrice = p.UnitPrice, QuantityOrder = s.QuantityOrder, Discount = s.Discount, TotalAmount = s.TotalAmount };

            if (shoppingCart.ToList().Count == 0)
            {
                PrintHelper.ShoppingCartIsEmpty();
                return;
            }

            decimal totalOrderAmount = OrderController.CalculateTotalOrderAmount();

            decimal totalOrderDiscount = OrderController.CalculateTotalOrderDiscount();

            decimal total = OrderController.CalculateTotal();

            PrintHelper.PrintShoppingCart(shoppingCart);       
            PrintHelper.PrintAdditionalInformations(totalOrderAmount, totalOrderDiscount, total);
            PrintHelper.PrintShoppingCartOptions();
            string opt = Console.ReadLine();
            switch (opt)
            {
                case "1":
                    _context.Orders.Clear();
                    PrintHelper.ShoppingCartIsEmpty();
                    break;
                case "2":
                    break;
                default:
                    PrintHelper.InvalidOption();
                    break;
            }
        }
    }
}
