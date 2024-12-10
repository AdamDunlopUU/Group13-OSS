// Nathan A Gallagher +
using System;
using System.IO;

namespace OnlineShopSystem
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public int StockQuantity { get; set; }

        // Constructor to initialize product properties
        public Product(int productId, string name, decimal price, int categoryId, int stockQuantity)
        {
            ProductID = productId;
            Name = name ?? throw new ArgumentNullException(nameof(name), "Product name cannot be null.");
            Price = price >= 0 ? price : throw new ArgumentException("Price cannot be negative.", nameof(price));
            CategoryID = categoryId;
            StockQuantity = stockQuantity >= 0 ? stockQuantity : throw new ArgumentException("Stock quantity cannot be negative.", nameof(stockQuantity));
        }

        // Add stock to the product
        public void AddStock(int quantity)
        {
            if (quantity > 0)
            {
                StockQuantity += quantity;
                Console.WriteLine($"{quantity} units added to {Name}. New stock quantity: {StockQuantity}");
            }
            else
            {
                Console.WriteLine("Invalid quantity. Cannot add zero or negative stock.");
            }
        }

        // Remove stock from the product
        public bool RemoveStock(int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Cannot remove zero or negative stock.");
                return false;
            }

            if (StockQuantity >= quantity)
            {
                StockQuantity -= quantity;
                Console.WriteLine($"{quantity} units removed from {Name}. Remaining stock: {StockQuantity}");
                return true;
            }
            else
            {
                Console.WriteLine($"Insufficient stock for {Name}. Available stock: {StockQuantity}");
                return false;
            }
        }

        // Update product details (name, price, category)
        public void UpdateProduct(string? name = null, decimal? price = null, int? categoryId = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Name = name;
            }

            if (price.HasValue && price >= 0)
            {
                Price = price.Value;
            }
            else if (price.HasValue)
            {
                Console.WriteLine("Invalid price. Price must be non-negative.");
            }

            if (categoryId.HasValue && categoryId >= 0)
            {
                CategoryID = categoryId.Value;
            }
            else if (categoryId.HasValue)
            {
                Console.WriteLine("Invalid category ID. Category ID must be non-negative.");
            }

            Console.WriteLine($"Product updated: {this}");
        }

        // Override ToString() method to display product details in a formatted way
        public override string ToString()
        {
            return $"ProductID: {ProductID}, Name: {Name}, Price: {Price:C}, CategoryID: {CategoryID}, StockQuantity: {StockQuantity}";
        }

        // Method to save product to CSV format (for external storage)
        public static void SaveProductToCSV(Product product, string fileName = "products.csv")
        {
            try
            {
                // Check if the file exists, if not create it with a header
                bool fileExists = File.Exists(fileName);
                using (var writer = new StreamWriter(fileName, append: true))
                {
                    // Write CSV header if the file doesn't exist
                    if (!fileExists)
                    {
                        writer.WriteLine("ProductID,Name,Price,CategoryID,StockQuantity");
                    }

                    // Write the product's data to the CSV
                    writer.WriteLine($"{product.ProductID},{product.Name},{product.Price},{product.CategoryID},{product.StockQuantity}");
                    Console.WriteLine($"Product saved to CSV: {product.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving product to CSV: {ex.Message}");
            }
        }
    }
}