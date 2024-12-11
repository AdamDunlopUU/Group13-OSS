using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OnlineShopSystem
{
    public class ShoppingControl
    {
        private List<User> customerList;
        private List<string> productList;
        private List<string> categoryList;
        private Dictionary<string, List<string>> productCategory;
        private List<string> adminList;

        public ShoppingControl()
        {
            customerList = new List<User>();
            productList = new List<string>();
            categoryList = new List<string>();
            adminList = new List<string>();  // List of Admins
            SetCategories();
            StoreProductCategory();

            // Load users from file
            LoadUsers();
        }

        // Load users from file or create default if file doesn't exist
        private void LoadUsers()
        {
            if (File.Exists("users.txt"))
            {
                var lines = File.ReadAllLines("users.txt");

                foreach (var line in lines)
                {
                    try
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 7)
                        {
                            int userID = int.Parse(parts[0]);
                            string userName = parts[1];
                            string userPassword = parts[2];
                            string userEmail = parts[3];
                            string phoneNumber = parts[4];
                            string userStreet = parts[5];
                            string userCity = parts[6];

                            // Add user to list
                            customerList.Add(new User(userID, userName, userPassword, userEmail, phoneNumber, userStreet, userCity));
                        }
                        else
                        {
                            Console.WriteLine($"Skipping invalid line: {line}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading user: {ex.Message}");
                    }
                }
            }
            else
            {
                // Default customer for testing if file does not exist
                customerList.Add(new User(1, "test", "test123", "test@example.com", "555-0000", "Test Street", "Test City"));
                SaveUsers();  // Save default user to file
            }
        }

        // Save users to file
        private void SaveUsers()
        {
            var lines = customerList.Select(u => $"{u.UserID},{u.UserName},{u.UserPassword},{u.UserEmail},{u.PhoneNumber},{u.UserStreet},{u.UserCity}");
            File.WriteAllLines("users.txt", lines);
        }

        // Store product categories
        public void StoreProductCategory()
        {
            productCategory = new Dictionary<string, List<string>>();
            foreach (var category in categoryList)
            {
                productCategory[category] = new List<string>();
            }
        }

        // Set predefined categories
        public void SetCategories()
        {
            categoryList = new List<string>
            {
                "smallCategory", "mediumCategory", "largeCategory", "specialCategory"
            };
        }

        // Admin login
        public void AdminLogin()
        {
            Console.Clear();
            Console.WriteLine("Admin Login");
            Console.Write("Enter Admin Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Admin Password: ");
            string password = Console.ReadLine();

            string adminUsername = "admin";
            string adminPassword = "admin123";

            if (username == adminUsername && password == adminPassword)
            {
                Console.WriteLine("Login successful.");
                AdminMenu();
            }
            else
            {
                Console.WriteLine("Invalid credentials. Press any key to try again.");
                Console.ReadKey();
            }
        }

        // Customer login
        public void CustomerLogin()
        {
            Console.Clear();
            Console.WriteLine("Customer Login");
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            // Validate against customer list
            User user = customerList.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && u.UserPassword == password);

            if (user != null)
            {
                Console.WriteLine("Login successful.");
                CustomerMenu();
            }
            else
            {
                Console.WriteLine("Invalid credentials. Press any key to try again.");
                Console.ReadKey();
            }
        }

        // Admin menu
        public void AdminMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Add a Product");
                Console.WriteLine("2. Remove a Product");
                Console.WriteLine("3. Manage Categories");
                Console.WriteLine("4. Manage Customers");
                Console.WriteLine("5. Logout");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        RemoveProduct();
                        break;
                    case "3":
                        ManageCategories();
                        break;
                    case "4":
                        ManageCustomers();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        private void AddProduct()
        {
            throw new NotImplementedException();
        }

        private void RemoveProduct()
        {
            throw new NotImplementedException();
        }

        private void ManageCategories()
        {
            throw new NotImplementedException();
        }

        private void ManageCustomers()
        {
            throw new NotImplementedException();
        }

        // Customer menu
        public void CustomerMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Customer Menu:");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. View Categories");
                Console.WriteLine("3. Logout");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewProducts();
                        break;
                    case "2":
                        ViewCategories();
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        // View products
        public void ViewProducts()
        {
            Console.WriteLine("Product List:");
            foreach (var product in productList)
            {
                Console.WriteLine(product);
            }
        }

        // View categories
        public void ViewCategories()
        {
            Console.WriteLine("Categories:");
            foreach (var category in categoryList)
            {
                Console.WriteLine($"- {category}");
            }
        }

        // Add a new customer
        public void AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("Add New Customer");
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            Console.Write("Enter phone number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter address: ");
            string address = Console.ReadLine();

            // Check for duplicate username
            if (customerList.Any(c => c.UserName == username))
            {
                Console.WriteLine("Username already exists.");
                return;
            }

            // Add the new customer
            int userID = customerList.Count + 1;  // Assign new userID
            User newCustomer = new User(userID, username, password, email, phoneNumber, address, address);
            customerList.Add(newCustomer);
            SaveUsers();  // Save customers to file
            Console.WriteLine("Customer added successfully.");
        }

        // View customers
        public void ViewCustomers()
        {
            Console.Clear();
            Console.WriteLine("Registered Customers:");
            foreach (var customer in customerList)
            {
                Console.WriteLine(customer.ToString());
            }
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        // Entry Point for the Application
        public static void Main(string[] args)
        {
            ShoppingControl shoppingControl = new ShoppingControl();

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Online Shop System");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Customer Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        shoppingControl.AdminLogin();
                        break;
                    case "2":
                        shoppingControl.CustomerLogin();
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}
