using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopSystem
{
    public class Admin : User
    {
        public DateTime LastLoggedIn { get; set; }

        // Password property is now private and managed internally
        private string Password { get; set; }

        // Constructor for Admin
        public Admin(int id, string userName, string password, string email, string phoneNumber, string addressStreet, string addressCity, DateTime lastLoggedIn)
            : base(id, userName, password, email, phoneNumber, addressStreet, addressCity)
        {
            LastLoggedIn = lastLoggedIn;
            Password = password;  // Assign the password during initialization
        }

        // Admin login method
        public static Admin AdminLogin(List<Admin> adminList)
        {
            Console.Clear();
            Console.WriteLine("===== Admin Login =====");

            // Hardcoded Admin Username and Password for testing
            string defaultUsername = "admin";
            string defaultPassword = "admin123";

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            // Check for hardcoded admin credentials
            if (username == defaultUsername && password == defaultPassword)
            {
                Console.WriteLine("Login successful!");
                return new Admin(1, "admin", "admin123", "admin@example.com", "555-1234", "123 Admin St", "Admin City", DateTime.Now);
            }
            else
            {
                // Check for matching admin in the list
                Admin admin = adminList.FirstOrDefault(a => a.UserName == username && a.Password == password);
                if (admin != null)
                {
                    Console.WriteLine("Login successful!");
                    admin.LastLoggedIn = DateTime.Now;
                    return admin;
                }
                else
                {
                    Console.WriteLine("Invalid credentials. Press any key to try again.");
                    Console.ReadKey();
                    return null;
                }
            }
        }

        // Main Admin Menu Method
        public void AdminDashboard(List<Product> productList, List<User> userList, List<Customer> customerList)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== ADMIN DASHBOARD =====");
                Console.WriteLine($"Welcome, {UserName}");
                Console.WriteLine("1. Product Management");
                Console.WriteLine("2. User Management");
                Console.WriteLine("3. View Reports");
                Console.WriteLine("4. Add Customer");
                Console.WriteLine("5. Logout");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProductManagementMenu(productList);
                        break;
                    case "2":
                        UserManagementMenu(userList);
                        break;
                    case "3":
                        ViewReports(productList, userList);
                        break;
                    case "4":
                        AddCustomer(customerList);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Add Customer Method
        private void AddCustomer(List<Customer> customerList)
        {
            Console.Clear();
            Console.WriteLine("===== ADD NEW CUSTOMER =====");

            Console.Write("Enter Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Enter Username: ");
            string userName = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Street Address: ");
            string addressStreet = Console.ReadLine();

            Console.Write("Enter City: ");
            string addressCity = Console.ReadLine();

            // Create a new Customer object
            Customer newCustomer = new Customer(customerId, userName, password, email, phoneNumber, addressStreet, addressCity);
            customerList.Add(newCustomer);

            Console.WriteLine("Customer added successfully!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Product Management Submenu
        private void ProductManagementMenu(List<Product> productList)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== PRODUCT MANAGEMENT =====");
                Console.WriteLine("1. Add New Product");
                Console.WriteLine("2. Update Existing Product");
                Console.WriteLine("3. Remove Product");
                Console.WriteLine("4. View All Products");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct(productList);
                        break;
                    case "2":
                        UpdateProduct(productList);
                        break;
                    case "3":
                        RemoveProduct(productList);
                        break;
                    case "4":
                        ViewAllProducts(productList);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void ViewAllProducts(List<Product> productList)
        {
            Console.Clear();
            Console.WriteLine("===== VIEW ALL PRODUCTS =====");

            if (productList.Count == 0)
            {
                Console.WriteLine("No products available.");
                Console.ReadKey();
                return;
            }

            foreach (var product in productList)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void AddProduct(List<Product> productList)
        {
            Console.Clear();
            Console.WriteLine("===== ADD NEW PRODUCT =====");

            Console.Write("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Product Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Product Category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Enter Product Stock Quantity: ");
            int stockQuantity = int.Parse(Console.ReadLine());

            Product newProduct = new Product(productId, name, price, categoryId, stockQuantity);
            productList.Add(newProduct);

            Console.WriteLine("Product added successfully!");
        }

        private void UpdateProduct(List<Product> productList)
        {
            Console.Clear();
            Console.Write("Enter Product ID to update: ");
            int productId = int.Parse(Console.ReadLine());

            Product productToUpdate = productList.FirstOrDefault(p => p.ProductID == productId);
            if (productToUpdate == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.WriteLine("Enter new product details (leave empty to keep current value):");

            Console.Write($"Name ({productToUpdate.Name}): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) productToUpdate.Name = name;

            Console.Write($"Price ({productToUpdate.Price}): ");
            string priceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(priceInput)) productToUpdate.Price = decimal.Parse(priceInput);

            Console.Write($"Category ID ({productToUpdate.CategoryID}): ");
            string categoryInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(categoryInput)) productToUpdate.CategoryID = int.Parse(categoryInput);

            Console.Write($"Stock Quantity ({productToUpdate.StockQuantity}): ");
            string stockInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(stockInput)) productToUpdate.StockQuantity = int.Parse(stockInput);

            Console.WriteLine("Product updated successfully!");
        }

        private void RemoveProduct(List<Product> productList)
        {
            Console.Clear();
            Console.Write("Enter Product ID to remove: ");
            int productId = int.Parse(Console.ReadLine());

            Product productToRemove = productList.FirstOrDefault(p => p.ProductID == productId);
            if (productToRemove != null)
            {
                productList.Remove(productToRemove);
                Console.WriteLine("Product removed successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        // User Management Submenu
        private void UserManagementMenu(List<User> userList)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== USER MANAGEMENT =====");
                Console.WriteLine("1. View All Users");
                Console.WriteLine("2. Add New User");
                Console.WriteLine("3. Remove User");
                Console.WriteLine("4. Return to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewAllUsers(userList);
                        break;
                    case "2":
                        AddNewUser(userList);
                        break;
                    case "3":
                        RemoveUser(userList);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void ViewAllUsers(List<User> userList)
        {
            Console.Clear();
            Console.WriteLine("===== CURRENT USER LIST =====");
            if (userList.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }

            foreach (var user in userList)
            {
                Console.WriteLine(user);
            }
        }

        private void AddNewUser(List<User> userList)
        {
            Console.Clear();
            Console.WriteLine("===== ADD NEW USER =====");

            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            Console.Write("Enter Username: ");
            string userName = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Street Address: ");
            string addressStreet = Console.ReadLine();

            Console.Write("Enter City: ");
            string addressCity = Console.ReadLine();

            User newUser = new User(userId, userName, password, email, phoneNumber, addressStreet, addressCity);
            userList.Add(newUser);

            Console.WriteLine("User added successfully!");
        }

        private void RemoveUser(List<User> userList)
        {
            Console.Clear();
            Console.WriteLine("===== REMOVE USER =====");

            Console.Write("Enter User ID to remove: ");
            int userId = int.Parse(Console.ReadLine());

            User userToRemove = userList.FirstOrDefault(u => u.UserID == userId);
            if (userToRemove != null)
            {
                userList.Remove(userToRemove);
                Console.WriteLine($"User {userToRemove.UserName} removed successfully.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        // View Reports Method
        private void ViewReports(List<Product> productList, List<User> userList)
        {
            Console.Clear();
            Console.WriteLine("===== VIEW REPORTS =====");

            Console.WriteLine("1. Total Products");
            Console.WriteLine("2. Total Users");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine($"Total Products: {productList.Count}");
                    break;
                case "2":
                    Console.WriteLine($"Total Users: {userList.Count}");
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        internal object Login(string? username, string? password)
        {
            throw new NotImplementedException();
        }
    }
}
