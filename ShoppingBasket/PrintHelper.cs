using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ShoppingBasket
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
                table.AddRow(product.Id, product.ProductName, string.Format(new CultureInfo("en-US"), "{0:C2}", product.UnitPrice));


            table.Write();
        }


    }
}
