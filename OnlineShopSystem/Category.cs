using OnlineShopSystem;
using System;
using System.Collections.Generic;
using System.Linq;

public class ShoppingControl
{
    private List<Customer> customerList;
    private List<string> productList;
    private List<string> categoryList;
    private Dictionary<string, List<string>> productCategory;
    private List<Admin> adminList;

    public ShoppingControl()
    {
        customerList = new List<Customer>();
        productList = new List<string>();
        categoryList = new List<string>();
        adminList = new List<Admin>();  // List of Admins
        SetCategories();
        StoreProductCategory();

        // Default admin (for testing purposes)
        adminList.Add(new Admin("admin", "admin123"));
    }

    public void StoreProductCategory()
    {
        productCategory = new Dictionary<string, List<string>>();
        foreach (var category in categoryList)
        {
            productCategory[category] = new List<string>();
        }
    }

    public void SetCategories()
    {
        categoryList = new List<string>
        {
            "smallCategory", "mediumCategory", "largeCategory", "specialCategory"
        };
    }

    public void AdminLogin()
    {
        Console.Clear();
        Console.WriteLine("Admin Login");
        Console.Write("Enter Admin Username: ");
        string username = Console.ReadLine();

        Console.Write("Enter Admin Password: ");
        string password = Console.ReadLine();

        // Fixed admin credentials
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

    public void CustomerLogin()
    {
        Console.Clear();
        Console.WriteLine("Customer Login");
        Console.Write("Enter Username: ");
        string username = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        Customer customer = customerList.FirstOrDefault(c => c.UserName == username && c.Password == password);
        if (customer != null)
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

    public void ManageCustomers()
    {
        Console.Clear();
        Console.WriteLine("Manage Customers");
        Console.WriteLine("1. Add a Customer");
        Console.WriteLine("2. View Customers");
        Console.WriteLine("3. Back to Admin Menu");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddCustomer();
                break;
            case "2":
                ViewCustomers();
                break;
            case "3":
                return;
            default:
                Console.WriteLine("Invalid option. Try again.");
                break;
        }
    }

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

        Customer newCustomer = new Customer(username, password, email, phoneNumber, address);
        customerList.Add(newCustomer);
        Console.WriteLine("Customer added successfully.");
    }

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

    public void ViewProducts()
    {
        Console.WriteLine("Product List:");
        foreach (var product in productList)
        {
            Console.WriteLine(product);
        }
    }

    public void ViewCategories()
    {
        Console.WriteLine("Categories:");
        foreach (var category in categoryList)
        {
            Console.WriteLine($"- {category}");
        }
    }

    public void AddProduct()
    {
        Console.WriteLine("Enter product name:");
        string product = Console.ReadLine();
        productList.Add(product);

        Console.WriteLine("Select a category for the product:");
        for (int i = 0; i < categoryList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categoryList[i]}");
        }

        int categorySelect = int.Parse(Console.ReadLine()) - 1;
        if (categorySelect >= 0 && categorySelect < categoryList.Count)
        {
            string category = categoryList[categorySelect];
            if (productCategory.ContainsKey(category))
            {
                productCategory[category].Add(product);
                Console.WriteLine($"{product} has been added to the {category} category.");
            }
        }
        else
        {
            Console.WriteLine("Invalid category selection.");
        }
    }

    public void RemoveProduct()
    {
        Console.WriteLine("Select a product to remove:");
        for (int i = 0; i < productList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {productList[i]}");
        }

        int delete = int.Parse(Console.ReadLine()) - 1;
        if (delete >= 0 && delete < productList.Count)
        {
            string product = productList[delete];
            productList.RemoveAt(delete);
            foreach (var category in productCategory.Values)
            {
                category.Remove(product);
            }
            Console.WriteLine($"{product} has been removed.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    public void ManageCategories()
    {
        Console.Clear();
        Console.WriteLine("Manage Categories");
        Console.WriteLine("1. View All Categories");
        Console.WriteLine("2. View Products in a Category");
        Console.WriteLine("3. Add a Category");
        Console.WriteLine("4. Remove a Category");
        Console.WriteLine("5. Back to Admin Menu");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ViewCategories();
                break;
            case "2":
                ViewProductsInCategory();
                break;
            case "3":
                AddCategory();
                break;
            case "4":
                RemoveCategory();
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Invalid option. Try again.");
                break;
        }
    }

    private void ViewProductsInCategory()
    {
        Console.WriteLine("Enter the category to view products:");
        string category = Console.ReadLine();
        if (productCategory.ContainsKey(category))
        {
            foreach (var product in productCategory[category])
            {
                Console.WriteLine($"- {product}");
            }
        }
        else
        {
            Console.WriteLine("No products found in this category.");
        }
    }

    public void AddCategory()
    {
        Console.WriteLine("Enter new category name:");
        string newCategory = Console.ReadLine();
        if (!categoryList.Contains(newCategory))
        {
            categoryList.Add(newCategory);
            productCategory[newCategory] = new List<string>();
            Console.WriteLine($"{newCategory} category added.");
        }
        else
        {
            Console.WriteLine("Category already exists.");
        }
    }

    public void RemoveCategory()
    {
        Console.WriteLine("Select a category to remove:");
        for (int i = 0; i < categoryList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categoryList[i]}");
        }

        int categoryToDelete = int.Parse(Console.ReadLine()) - 1;
        if (categoryToDelete >= 0 && categoryToDelete < categoryList.Count)
        {
            string category = categoryList[categoryToDelete];
            categoryList.RemoveAt(categoryToDelete);
            productCategory.Remove(category);
            Console.WriteLine($"{category} category has been removed.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }
}
