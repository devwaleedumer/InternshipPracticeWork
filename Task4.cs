using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    internal class Program
    {
        // =========================================
        // Fields / Properties
        // =========================================

        public static int ProductIdPointer { get; set; }
        public static List<Product> Products { get; set; } = [];

        // Constructor 

        public Program()
        {

        }

        // =========================================
        // Main
        // =========================================

        static async Task Main(string[] args)
        {
            bool isRunning = true;

            SeedProducts();

            ProductIdPointer = Products.Max(p => p.Id);

            do
            {
                ShowMenu();

                int choice = ReadInt("Enter Your Choice");

                switch (choice)
                {
                    case 1:
                        ShowProducts();
                        break;

                    case 2:
                        SearchProducts();
                        break;

                    case 3:
                        await AddProduct();
                        break;

                    case 4:
                        UpdateProduct();
                        break;

                    case 5:
                        RemoveProduct(Products);
                        break;

                    case 6:
                        SortProductsByPrice(Products);
                        break;

                    case 7:
                        ShowLowStockProducts(Products);
                        break;

                    case 8:
                        ShowTotalInventoryValue(Products);
                        break;

                    case 9:
                        isRunning = false;
                        break;

                    default:
                        ShowError("Please enter a valid choice.");
                        break;
                }

            } while (isRunning);
        }

        // =========================================
        // CRUD OPERATIONS
        // =========================================

        static async Task AddProduct()
        {
            DisplaySection("Add Product");

            Product newProduct = InputAndValidateProduct();

            newProduct.Id = ++ProductIdPointer;

            Products.Add(newProduct);

            ShowSuccess($"Product '{newProduct.Name}' added successfully.");

            await SaveInventoryAsync(Products.Count);
        }

        static void UpdateProduct()
        {
            DisplaySection("Update Product");

            var productToUpdate = FindProductById(Products);

            var updatedProduct = InputAndValidateProductForUpdate(productToUpdate);

            productToUpdate.Name = updatedProduct.Name;
            productToUpdate.Price = updatedProduct.Price;
            productToUpdate.Stock = updatedProduct.Stock;

            ShowSuccess($"Product with Id {productToUpdate.Id} updated successfully.");
        }

        static void RemoveProduct(List<Product> products)
        {
            DisplaySection("Remove Product");

            var productToRemove = FindProductById(Products);

            Products.Remove(productToRemove);

            ShowSuccess($"Product '{productToRemove.Name}' removed successfully.");
        }

        static void ShowProducts()
        {
            DisplaySection("All Products");

            if (Products.Count == 0)
            {
                ShowError("No products available.");
                return;
            }

            Products.ForEach(p => Console.WriteLine(FormatProduct(p)));
        }

        // =========================================
        // SEARCHING / SORTING / FILTERING
        // =========================================

        static void SearchProducts()
        {
            DisplaySection("Search Product");

            if (Products.Count == 0)
            {
                ShowError("No products available.");
                return;
            }

            string searchTerm = ReadText("Enter product name to search");

            var searchedProducts = Products
                .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (searchedProducts.Count == 0)
            {
                ShowError("No matching products found.");
                return;
            }

            searchedProducts.ForEach(p => Console.WriteLine(FormatProduct(p)));
        }

        static void SortProductsByPrice(List<Product> products)
        {
            DisplaySection("Products Sorted by Price (High to Low)");

            Products
                .OrderByDescending(p => p.Price)
                .ToList()
                .ForEach(p => Console.WriteLine(FormatProduct(p)));
        }

        static void ShowLowStockProducts(List<Product> products)
        {
            DisplaySection("Low Stock Products (Stock < 5)");

            var lowStockProducts = Products
                .Where(p => p.Stock < 5)
                .OrderBy(p => p.Stock)
                .ToList();

            if (lowStockProducts.Count == 0)
            {
                ShowSuccess("No low stock products.");
                return;
            }

            lowStockProducts.ForEach(p => Console.WriteLine(FormatProduct(p)));
        }

        static void ShowTotalInventoryValue(List<Product> products)
        {
            DisplaySection("Total Inventory Value");

            decimal totalValue = Products.Sum(p => p.Price * p.Stock);

            Console.WriteLine($"Total Inventory Value: {totalValue:C}");
        }

        // =========================================
        // VALIDATION
        // =========================================

        static IEnumerable<string> ValidateProduct(Product product)
        {
            if (product is null)
            {
                yield return "Product cannot be null.";
                yield break;
            }

            if (string.IsNullOrWhiteSpace(product.Name))
                yield return "Product name cannot be empty.";

            if (product.Price <= 0)
                yield return "Product price must be greater than zero.";

            if (product.Stock < 0)
                yield return "Product stock cannot be negative.";
        }

        // =========================================
        // INPUT METHODS
        // =========================================

        static Product InputAndValidateProduct()
        {
            while (true)
            {
                string name = ReadText("Enter product name");

                decimal price = ReadDecimal("Enter product price");

                int stock = ReadInt("Enter product stock");

                var product = new Product(name, price, stock);

                var errors = ValidateProduct(product).ToList();

                if (errors.Count == 0)
                    return product;

                PrintErrors(errors);
            }
        }

        static Product InputAndValidateProductForUpdate(Product defaultProduct)
        {
            while (true)
            {
                string name = ReadText(
                    $"Enter product name ({defaultProduct.Name})",
                    false);

                decimal price = ReadDecimal(
                    $"Enter product price ({defaultProduct.Price})",
                    false);

                int stock = ReadInt(
                    $"Enter product stock ({defaultProduct.Stock})",
                    false);

                var updatedProduct = new Product(
                    string.IsNullOrWhiteSpace(name)
                        ? defaultProduct.Name
                        : name,

                    price <= 0
                        ? defaultProduct.Price
                        : price,

                    stock < 0
                        ? defaultProduct.Stock
                        : stock
                );

                var errors = ValidateProduct(updatedProduct).ToList();

                if (errors.Count == 0)
                    return updatedProduct;

                PrintErrors(errors);
            }
        }

        static Product FindProductById(List<Product> products)
        {
            while (true)
            {
                int id = ReadInt("Enter product Id");

                var product = Products.FirstOrDefault(p => p.Id == id);

                if (product is not null)
                    return product;

                ShowError($"Product with Id {id} does not exist.");
            }
        }

        static string ReadText(string text, bool validate = true)
        {
            while (true)
            {
                Console.Write($"{text}: ");

                string? input = Console.ReadLine()?.Trim();

                if (!validate)
                    return input ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                ShowError("Invalid input. Please try again.");
            }
        }

        static int ReadInt(string text, bool validation = true)
        {
            while (true)
            {
                Console.Write($"{text}: ");

                string? input = Console.ReadLine();

                if (!validation)
                {
                    if (string.IsNullOrWhiteSpace(input))
                        return 0;

                    if (int.TryParse(input, out int optionalResult))
                        return optionalResult;

                    ShowError("Invalid number.");
                    continue;
                }

                if (int.TryParse(input, out int result))
                    return result;

                ShowError($"{input} is not a valid whole number.");
            }
        }

        static decimal ReadDecimal(string text, bool validation = true)
        {
            while (true)
            {
                Console.Write($"{text}: ");

                string? input = Console.ReadLine();

                if (!validation)
                {
                    if (string.IsNullOrWhiteSpace(input))
                        return 0;

                    if (decimal.TryParse(input, out decimal optionalResult))
                        return optionalResult;

                    ShowError("Invalid decimal number.");
                    continue;
                }

                if (decimal.TryParse(input, out decimal result))
                    return result;

                ShowError($"{input} is not a valid decimal number.");
            }
        }

        static void SeedProducts()
        {
            Products = new()
            {
                new Product(1,  "Wireless Mouse",      29.99m, 150),
                new Product(2,  "Mechanical Keyboard", 79.99m, 85),
                new Product(3,  "USB-C Hub",           45.00m, 200),
                new Product(4,  "27\" Monitor",        299.99m, 40),
                new Product(5,  "Webcam HD",           59.99m, 120),
                new Product(6,  "Laptop Stand",        34.99m, 175),
                new Product(7,  "Headphones",          149.99m, 60),
                new Product(8,  "External SSD 1TB",    109.99m, 95),
                new Product(9,  "HDMI Cable 2m",       12.99m, 300),
                new Product(10, "Desk Lamp LED",       24.99m, 130)
             };
        }
        // =========================================
        // DISPLAY / UI HELPERS
        // =========================================

        static void ShowMenu()
        {
            DisplaySection("Inventory Management System");

            Console.WriteLine("1. View All Products");
            Console.WriteLine("2. Search Product");
            Console.WriteLine("3. Add Product");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Remove Product");
            Console.WriteLine("6. Sort Products By Price");
            Console.WriteLine("7. Show Low Stock Products");
            Console.WriteLine("8. Show Total Inventory Value");
            Console.WriteLine("9. Exit");

            Console.WriteLine();
        }

        static void DisplaySection(string title)
        {
            Console.WriteLine($"\n{new string('=', 60)}");
            Console.WriteLine(title);
            Console.WriteLine(new string('=', 60));
        }

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }

        static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void PrintErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ShowError(error);
            }

            Console.WriteLine();
        }

        // =========================================
        // FORMATTERS
        // =========================================

        static string FormatProduct(Product p) =>
            $"{p.Id,-3} Product: {p.Name,-30} " +
            $"Price: {p.Price,10:C} " +
            $"Stock: {p.Stock,5}";

        // =========================================
        // DATA / STORAGE
        // =========================================

        static async Task SaveInventoryAsync(int count)
        {
            Console.WriteLine("Saving data into cloud...");

            await Task.Delay(2000);

            ShowSuccess($"{count} items saved successfully.");
        }

        // =========================================
        // MODELS
        // =========================================

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

            public Product(string name, decimal price, int stock)
            {
                Name = name;
                Price = price;
                Stock = stock;
            }
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