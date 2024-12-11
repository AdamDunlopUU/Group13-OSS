using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopSystem
{
    public class Admin : User
    {
        public DateTime LastLoggedIn { get; set; }
        private string Password { get; set; }

        // Full constructor
        public Admin(int id, string userName, string password, string email, string phoneNumber, string addressStreet, string addressCity, DateTime lastLoggedIn)
            : base(id, userName, password, email, phoneNumber, addressStreet, addressCity)
        {
            LastLoggedIn = lastLoggedIn;
            Password = password;
        }

        // Simplified constructor for minimal initialization
        public Admin(string userName, string password)
            : base(0, userName, password, null, null, null, null)
        {
            Password = password;
        }

        public static Admin AdminLogin(List<Admin> adminList)
        {
            Console.Clear();
            Console.WriteLine("===== Admin Login =====");

            string defaultUsername = "admin";
            string defaultPassword = "admin123";

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (username == defaultUsername && password == defaultPassword)
            {
                Console.WriteLine("Login successful!");
                return new Admin(1, "admin", "admin123", "admin@example.com", "555-1234", "123 Admin St", "Admin City", DateTime.Now);
            }
            else
            {
                var admin = adminList.FirstOrDefault(a => a.UserName == username && a.Password == password);
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

            Customer newCustomer = new Customer(customerId, userName, password, email, phoneNumber, addressStreet, addressCity);
            customerList.Add(newCustomer);

            Console.WriteLine("Customer added successfully!");
            Console.ReadKey();
        }

        private void ProductManagementMenu(List<Product> productList)
        {
            // Implement Product Management Menu logic here...
        }

        private void UserManagementMenu(List<User> userList)
        {
            // Implement User Management Menu logic here...
        }

        private void ViewReports(List<Product> productList, List<User> userList)
        {
            // Implement Reports Viewing logic here...
        }
    }
}
