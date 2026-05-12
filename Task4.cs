using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            var products = GetAllProducts();
           
            DisplaySection("All Products Sorted by Price (High to Low)");

            var sortedProducts = products
                .OrderByDescending(p => p.Price)
                .ToList();

            ShowProducts(sortedProducts);


            DisplaySection("Low Stock Products (Stock < 5)");

            var filteredProducts = products
                .Where(p => p.Stock < 5)
                .OrderBy(p => p.Stock)
                .ToList();

            ShowProducts(filteredProducts);

            
            DisplaySection("Search Product");

            var searchedProduct = products
                .FirstOrDefault(p => p.Name.Contains("HDMI"));

            Console.WriteLine(
                searchedProduct is not null
                    ? FormatProduct(searchedProduct)
                    : "Product not found.");


            DisplaySection("Total Inventory Value");

            var totalValue = products.Sum(p => p.Price * p.Stock);

            Console.WriteLine($"Total Inventory Value: {totalValue:C}");

            // Save Inventory
            await SaveInventoryAsync(products.Count);

            Console.ReadKey();
        }

        static async Task SaveInventoryAsync(int count)
        {
            Console.WriteLine("Saving data into cloud...");
            await Task.Delay(3000);
            Console.WriteLine($"{count} Items saved successfully");
        }

        static void ShowProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine(FormatProduct(product));
            }
        }

        static string FormatProduct(Product p) => $"{p.Id,-3} Product: {p.Name,-30} " +  $"Price: {p.Price,8:C} " + $"Stock: {p.Stock,5}";

        static void DisplaySection(string title)
        {
            Console.WriteLine($"\n{new string('=', 60)}");
            Console.WriteLine(title);
            Console.WriteLine(new string('=', 60));
        }
        static List<Product> GetAllProducts() => new()
            {
                new Product(1,  "Wireless Mouse",        29.99m,  150),
                new Product(2,  "Mechanical Keyboard",   79.99m,  85),
                new Product(3,  "USB-C Hub",             45.00m,  200),
                new Product(4,  "27\" Monitor",          299.99m, 40),
                new Product(5,  "Webcam HD",             59.99m,  120),
                new Product(6,  "Laptop Stand",          34.99m,  175),
                new Product(7,  "Noise Cancelling Headphones", 149.99m, 60),
                new Product(8,  "External SSD 1TB",      109.99m, 95),
                new Product(9,  "HDMI Cable 2m",         12.99m,  300),
                new Product(10, "Desk Lamp LED",         24.99m,  130),
                new Product(11, "Mousepad XL",           19.99m,  220),
                new Product(12, "Graphics Tablet",       199.99m, 35),
                new Product(13, "Portable Charger",      39.99m,  180),
                new Product(14, "Smart Speaker",         89.99m,  70),
                new Product(15, "Cable Management Box",  15.99m,  250),
                new Product(16, "Ergonomic Chair",       399.99m, 20),
                new Product(17, "Standing Desk",         549.99m, 15),
                new Product(18, "Microphone USB",        74.99m,  90),
                new Product(19, "Screen Cleaning Kit",   9.99m,   400),
                new Product(20, "Ethernet Cable 5m",     14.99m,  310),
                new Product(3,  "USB-C Hub",             45.00m,  2),
                new Product(9,  "HDMI Cable 2m",         12.99m,  4),
                new Product(17, "Standing Desk",         549.99m, 1),
            };

    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public Product(int id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}


//Q1) - Lambda Expressions: What is the p => p.Name syntax and how does it work with LINQ?
//lambda expressions are anonymous functions that can be used to create delegates or expression tree types.
//In the context of LINQ, they are often used to specify the criteria for filtering, sorting, or projecting data.
//The syntax p => p.Name is a lambda expression where p represents an individual element in a collection,
//and p.Name accesses the Name property of that element.
//This allows you to perform operations like sorting by name or filtering based on the name property in a concise and readable way.

//Q2) LINQ vs. Loops: Why is LINQ often preferred over foreach for filtering data?
// LINQ is often preferred over foreach loops for filtering data because it provides a more declarative and expressive syntax.

//Q3) Task.Delay vs Thread.Sleep: Why is one "non-blocking" and the other "blocking"?
//Task.Delay is non-blocking because it allows the current thread to continue executing other code while waiting for the specified time to elapse.
//It returns a Task that can be awaited, allowing for asynchronous programming.
//Thread.Sleep, on the other hand, is blocking because it halts the execution of the current thread for the specified duration.
//It does not allow the thread to perform any other operations during that time,
//which can lead to unresponsive applications if used in the main thread.

//Q4) Value vs. Reference Types: Why does changing a property on a Product object inside a List update the actual item in that list?
//In C#, classes are reference types, which means that when you create an instance of a class (like Product) and add it to a List,
// you are actually adding a reference to that object in the list. When you change a property of that object through the reference,
// you are modifying the same object that is stored in the list.
// If Product were a struct (a value type), then changing a property would not affect the original item in the list because structs are copied when passed around
// and you would be modifying a copy rather than the original object.

// What is the purpose of the async and await keywords? What would happen if you ran a 10-second "Save" operation without using async in a real-world UI application?
// The async keyword is used to declare a method as asynchronous, allowing it to run operations that may take time (like I/O operations) without blocking the main thread.
// The await keyword is used to pause the execution of an async method until the awaited Task completes, allowing other code to run in the meantime.
// If you ran a 10-second "Save" operation without using async in a real-world UI application, the main thread would be blocked for those 10 seconds.
// This would make the application unresponsive, as the UI would not be able to process user input or update during that time, leading to a poor user experience.