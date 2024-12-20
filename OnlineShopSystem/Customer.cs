﻿using OnlineShopSystem;

public class Customer : User
{
    private string? address;

    public string Status { get; set; } = "active"; // Default status is "active"
    public string Role { get; set; } = "Customer"; // Default role is "Customer"
    public string? Password { get; private set; }

    public ShoppingBasket Basket { get; set; } = new ShoppingBasket();

    // Constructor
    public Customer(int id, string userName, string password, string email, string phoneNumber, string addressStreet, string addressCity, string status = "active")
        : base(id, userName, password, email, phoneNumber, addressStreet, addressCity)
    {
        Status = status;
    }

    public Customer(string? username, string? password, string? email, string? phoneNumber, string? address)
        : base(0, username, password, email, phoneNumber, address ?? "", "")
    {
        this.address = address;
    }

    // Customer login method with automatic test customer
    public static Customer? CustomerLogin(List<Customer> customerList)
    {
        Console.Clear();
        Console.WriteLine("===== Customer Login =====");

        Console.Write("Enter Username: ");
        string username = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Enter Password: ");
        string password = Console.ReadLine()?.Trim() ?? "";

        // Check if the user entered "test" and "test123" for automatic login
        if (username == "test" && password == "test123")
        {
            Console.WriteLine("Login successful! You are logged in as the test customer.");
            return new Customer(1, "test", "test123", "test@example.com", "123-456-7890", "123 Test St", "Test City");
        }

        // Search for the customer with the provided username and password
        Customer? customer = customerList.FirstOrDefault(c => c.UserName == username && c.Password == password);

        if (customer != null)
        {
            Console.WriteLine("Login successful!");
            return customer;
        }
        else
        {
            Console.WriteLine("Invalid credentials. Press any key to try again.");
            Console.ReadKey();
            return null;
        }
    }

    // Main Customer Menu Method
    public void CustomerDashboard(List<Product> productList)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== CUSTOMER DASHBOARD =====");
            Console.WriteLine($"Welcome, {UserName}");
            Console.WriteLine("1. Browse Products");
            Console.WriteLine("2. View Basket");
            Console.WriteLine("3. Checkout");
            Console.WriteLine("4. Logout");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    BrowseProducts(productList);
                    break;
                case "2":
                    ViewBasket();
                    break;
                case "3":
                    Checkout();
                    return; // End the customer session
                case "4":
                    return; // End the customer session
                default:
                    Console.WriteLine("Invalid option. Press any key to continue.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Browse Products method
    private void BrowseProducts(List<Product> productList)
    {
        Console.Clear();
        Console.WriteLine("===== BROWSE PRODUCTS =====");

        if (productList.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        foreach (var product in productList)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("Enter Product ID to add to your basket or 0 to go back:");

        if (!int.TryParse(Console.ReadLine(), out int productId) || productId < 0)
        {
            Console.WriteLine("Invalid input. Press any key to return.");
            Console.ReadKey();
            return;
        }

        if (productId == 0)
        {
            return; // Go back to the previous menu
        }

        Product? selectedProduct = productList.FirstOrDefault(p => p.ProductID == productId);
        if (selectedProduct != null)
        {
            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Basket.AddItem(selectedProduct, quantity);
            Console.WriteLine($"{selectedProduct.Name} added to your basket.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    // View Basket method
    private void ViewBasket()
    {
        Console.Clear();
        Console.WriteLine("===== VIEW BASKET =====");
        Basket.DisplayBasket(); // Display the contents of the basket
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    // Checkout method
    private void Checkout()
    {
        Console.Clear();
        Console.WriteLine("===== CHECKOUT =====");
        if (Basket.Items.Count == 0)
        {
            Console.WriteLine("Your basket is empty. Add items to proceed with checkout.");
        }
        else
        {
            Console.WriteLine($"Total Price: {Basket.GetTotalPrice():C}");
            // Proceed with checkout logic (e.g., payment, order confirmation) here
            Console.WriteLine("Proceeding with checkout...");
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
