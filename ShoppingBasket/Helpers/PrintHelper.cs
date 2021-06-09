using ConsoleTables;
using ShoppingBasket.Entities;
using ShoppingBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Helpers
{
    public static class PrintHelper
    {
        public static void WelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("\nShopping Basket App");
            Console.WriteLine("1. Browse all products");
            Console.WriteLine("2. View shopping cart");
            Console.Write("Enter your option: ");
        }

        public static void PrintMessage(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PrintPoducts (List<Product> productList)
        {
            Console.Clear();

            var table = new ConsoleTable("Product Id", "Product Name", "Unit Price");

            foreach (var product in productList)
                table.AddRow(product.Id, product.ProductName, FormatHelper.FormatAmount(product.UnitPrice));


            table.Write();
        }

        public static void PrintShoppingCart(IEnumerable<ShoppingCartModel> shoppingCart)
        {
            var table = new ConsoleTable("Product Name", "Price", "Quantity", "Total", "Discount", "Total with discount");

            Console.WriteLine("Total Items in Shopping Cart: " + shoppingCart.Count());
            foreach (var item in shoppingCart)
                table.AddRow(item.ProductName, FormatHelper.FormatAmount(item.UnitPrice), item.QuantityOrder, FormatHelper.FormatAmount(item.TotalAmount), FormatHelper.FormatAmount(item.Discount), FormatHelper.FormatAmount(item.TotalAmount - item.Discount));

            table.Options.EnableCount = false;
            table.Write();
            Console.WriteLine();
        }

        public static void PrintAdditionalInformations(decimal totalOrderAmount, decimal totalOrderDiscount, decimal total)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Total Order Amount: " + FormatHelper.FormatAmount(totalOrderAmount));
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Total Order Discount: " + FormatHelper.FormatAmount(totalOrderDiscount));
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Total With Discount : " + FormatHelper.FormatAmount(total));
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
        }

        public static void EnterProductId ()
        {
            Console.Write("Enter the product ID you want to buy: ");
        }

        public static void EnterQuantity()
        {
            Console.Write("Enter quantity to buy: ");
        }

        public static void ProductNotFound()
        {
            PrintMessage("Product not found.", ConsoleColor.Red);
        }

        public static void AddedIntoShoppingCart (string productName)
        {
            PrintMessage($"{productName} added into shopping cart.", ConsoleColor.Yellow);
        }

        public static void InvalidOption()
        {
            PrintMessage("Invalid option.", ConsoleColor.Red);
        }

        public static void ShoppingCartIsEmpty()
        {
            PrintMessage("Shopping cart is empty. Go to browse products.", ConsoleColor.Yellow);
        }

        public static void PrintShoppingCartOptions()
        {
            Console.WriteLine("1. Clear shopping cart.");
            Console.WriteLine("2. Back.");
            Console.Write("Enter option: ");
        }
    }
}
